using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NetDevPack.Mediator;
using Play.Application.Core.Interfaces;
using Play.Application.Core.ViewModels;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;

namespace Play.Application.Core.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IMapper mapper, IMediatorHandler mediatorHandler,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _logger = logger;
    }

    public async Task<UserViewModel> GetAsync(Guid id)
    {
        return _mapper.Map<UserViewModel>(await _userRepository.GetByIdAsync(id));
    }

    /// <summary>
    ///     Get All Users from Database with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetAllAsync(int page, int pageSize)
    {
        return await _userRepository.GetAllAsync(page, pageSize);
    }

    /// <summary>
    ///     Function to fetch the user based on the email address
    /// </summary>
    /// <param name="email">Email address</param>
    /// <returns></returns>
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    /// <summary>
    ///     Login User with email and password
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns>Tuple with result and last login date</returns>
    public async Task<(bool, DateTime)> LoginAsync(UserViewModel userViewModel)
    {
        var user = await _userRepository.GetByEmailAsync(userViewModel.Email);
        if (user == null) return (false, DateTime.MinValue);
        //validate password
        var valid = _userRepository.CheckPassword(userViewModel.Password, user.PasswordHash, user.Salt);
        if (!valid)
        {
            //check if user is logging in with Otp
            if (user.OtpSecret == null || user.OtpSecret.Equals(string.Empty)) return (false, DateTime.MinValue);
            //check if password is otp
            valid = userViewModel.Password == user.OtpSecret;
        }

        if (valid)
        {
            //increment login count
            user.LoginAttempts++;
            var lastLogin = user.LastLogin ?? DateTime.MinValue;
            user.LastLogin = DateTime.Now;
            //set otp secret to null
            user.OtpSecret = null;
            _userRepository.Update(user);
            await _userRepository.UnitOfWork.Commit();
            return (true, lastLogin);
        }

        //increment failed login count
        user.FailedLoginAttempts++;
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.Commit();
        //log failed login
        _logger.LogWarning($"Failed login attempt for user {user.Email}");

        return (false, DateTime.MinValue);
    }
    
    /// <summary>
    ///     Login WHMCS User/Bot with email and password
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns>Tuple with result and last login date</returns>
    public async Task<(bool, DateTime)> LoginBotAsync(UserViewModel userViewModel)
    {
        var user = await _userRepository.GetByEmailAsync(userViewModel.Email);
        if (user == null) return (false, DateTime.MinValue);
        //validate password
        var valid = _userRepository.CheckPassword(userViewModel.Password, user.PasswordHash, user.Salt);
        if (!valid)
        {
            //check if user is logging in with Otp
            if (user.OtpSecret == null || user.OtpSecret.Equals(string.Empty)) return (false, DateTime.MinValue);
            //check if password is otp
            valid = userViewModel.Password == user.OtpSecret;
        }

        if (valid && user.Role == "PlayBot")
        {
            //increment login count
            user.LoginAttempts++;
            var lastLogin = user.LastLogin ?? DateTime.MinValue;
            user.LastLogin = DateTime.Now;
            //set otp secret to null
            user.OtpSecret = null;
            _userRepository.Update(user);
            await _userRepository.UnitOfWork.Commit();
            return (true, lastLogin);
        }

        //increment failed login count
        user.FailedLoginAttempts++;
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.Commit();
        //log failed login
        _logger.LogWarning($"Failed login attempt for WHMCS User Bot {user.Email}");

        return (false, DateTime.MinValue);
    }

    /// <summary>
    ///     Get total count of users
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetTotalCountAsync()
    {
        return await _userRepository.GetTotalCount();
    }

    /// <summary>
    ///     Get total count of users with a given role
    ///     Optionally pass a time range to get count of users with a given role with CreatedAt in that range
    /// </summary>
    /// <param name="role"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<int> GetTotalCountByRoleAsync(string role, DateTime? from = null, DateTime? to = null)
    {
        return await _userRepository.GetTotalCountByRoleAsync(role, from, to);
    }

    /// <summary>
    ///     Dispatch register command
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Register(UserViewModel userViewModel)
    {
        var registerCommand = _mapper.Map<RegisterUserCommand>(userViewModel);
        var res = await _mediatorHandler.SendCommand(registerCommand);
        return res;
    }

    /// <summary>
    ///     Update a user entity
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Update(UpdateUserViewModel userViewModel)
    {
        var updateCommand = _mapper.Map<UpdateUserCommand>(userViewModel);
        var res = await _mediatorHandler.SendCommand(updateCommand);
        return res;
    }

    /// <summary>
    ///     Dispatch command to update user password
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns></returns>
    public async Task<ValidationResult> UpdatePassword(UpdatePasswordUserViewModel userViewModel)
    {
        var updateCommand = _mapper.Map<UpdateUserPasswordCommand>(userViewModel);
        var res = await _mediatorHandler.SendCommand(updateCommand);
        return res;
    }

    /// <summary>
    ///     Dispatch a command to start the forgot password process
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns></returns>
    public async Task<ValidationResult> ForgotPassword(UserForgotPasswordViewModel userViewModel)
    {
        var updateCommand = _mapper.Map<ForgotPasswordCommand>(userViewModel);
        var res = await _mediatorHandler.SendCommand(updateCommand);
        return res;
    }

    /// <summary>
    ///     Dispatch a command to remove a user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveUserCommand(id);
        var res = await _mediatorHandler.SendCommand(removeCommand);
        return res;
    }

    /// <summary>
    ///     Update user's role
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns></returns>
    public Task<ValidationResult> UpdateRole(UpdateRoleUserViewModel userViewModel)
    {
        var updateCommand = _mapper.Map<UpdateUserRoleCommand>(userViewModel);
        var res = _mediatorHandler.SendCommand(updateCommand);
        return res;
    }

    /// <summary>
    ///     Get count of users created within a given time range
    /// </summary>
    /// <param name="startDateTime"></param>
    /// <param name="endDateTime"></param>
    /// <returns></returns>
    public async Task<int> GetCountByCreatedAtAsync(DateTime startDateTime, DateTime endDateTime)
    {
        return await _userRepository.GetTotalCountByTimeRangeAsync(startDateTime, endDateTime);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}