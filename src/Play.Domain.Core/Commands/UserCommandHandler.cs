using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using Play.Domain.Core.Events;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;

namespace Play.Domain.Core.Commands;

public class UserCommandHandler : CommandHandler, IRequestHandler<RegisterUserCommand, ValidationResult>,
    IRequestHandler<UpdateUserCommand, ValidationResult>,
    IRequestHandler<RemoveUserCommand, ValidationResult>,
    IRequestHandler<UpdateUserPasswordCommand, ValidationResult>,
    IRequestHandler<ForgotPasswordCommand, ValidationResult>,
    IRequestHandler<UpdateUserRoleCommand, ValidationResult>
{
    private readonly ILogger<UserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;


    public UserCommandHandler(IUserRepository userRepository, ILogger<UserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Handle user password reset
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //check that user exists
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            //log warning
            _logger.LogWarning("Forgot password attempt for non-existent user - {request}", request);
            AddError("User not found");
            return ValidationResult;
        }

        //update Otp 
        user.OtpSecret = Guid.NewGuid().ToString();

        //pass to domain
        user.AddDomainEvent(new ForgotPasswordEvent(user.Id, request.Email, user.OtpSecret));

        //pass to repository
        _userRepository.Update(user);

        //commit unit of work
        return await Commit(_userRepository.UnitOfWork);
    }

    /// <summary>
    ///     Handle user registration
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        // Validation logic here

        //check if email already exists
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user != null)
        {
            AddError("Email already exists");
            return ValidationResult;
        }

        //hash password
        var hashAndSalt = _userRepository.HashPassword(request.Password);


        //create user object
        var newUser = new User(Guid.NewGuid(), request.Email, hashAndSalt.Item1, hashAndSalt.Item2, request.Email,
            "Customer", 0, 0, DateTime.Now, string.Empty, DateTime.Now);

        //pass to domain
        newUser.AddDomainEvent(new UserRegisteredEvent(newUser.Id, newUser.Email, newUser.PasswordHash, newUser.Salt,
            newUser.Username, newUser.LoginAttempts, newUser.FailedLoginAttempts, newUser.LastLogin,
            newUser.CreatedAt));

        //pass to repository
        _userRepository.Add(newUser);

        //commit unit of work
        return await Commit(_userRepository.UnitOfWork);
    }

    /// <summary>
    ///     Handle user removal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //get user from repository by id
        var userToRemove = await _userRepository.GetByIdAsync(request.Id);

        //pass to domain
        userToRemove.AddDomainEvent(new UserRemovedEvent(userToRemove.Id, userToRemove.Email));
        //pass to repository
        _userRepository.Remove(userToRemove);
        //commit unit of work
        return await Commit(_userRepository.UnitOfWork);
    }

    /// <summary>
    ///     Handle user update
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task<ValidationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //check if email already exists and is not the same as the one being updated
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user != null && user.Id != request.Id)
        {
            //log warning
            _logger.LogWarning("Attempted to update user with email that already exists - {request}", request);
            AddError("Email already exists");
            return ValidationResult;
        }

        //get user from repository by id
        var userToUpdate = await _userRepository.GetByIdAsync(request.Id);

        //update user password hash and salt
        var hashAndSalt = _userRepository.HashPassword(request.Password);
        userToUpdate.Salt = hashAndSalt.Item2;
        userToUpdate.PasswordHash = hashAndSalt.Item1;
        userToUpdate.Email = request.Email;
        userToUpdate.Username = request.Email;

        //pass to domain
        userToUpdate.AddDomainEvent(new UserUpdatedEvent(userToUpdate.Id, userToUpdate.Email, userToUpdate.PasswordHash,
            userToUpdate.Salt, userToUpdate.Username, userToUpdate.LoginAttempts, userToUpdate.FailedLoginAttempts,
            userToUpdate.LastLogin, userToUpdate.CreatedAt));
        //pass to repository
        _userRepository.Update(userToUpdate);
        //commit unit of work
        return await Commit(_userRepository.UnitOfWork);
    }

    /// <summary>
    ///     Handle user password change
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //check that user exists
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            //log warning
            _logger.LogWarning("Attempted to update password for non-existent user - {request}", request);
            AddError("User not found");
            return ValidationResult;
        }

        //check that password is correct
        if (!_userRepository.CheckPassword(request.Old_Password, user.PasswordHash, user.Salt))
        {
            //log warning
            _logger.LogWarning("Attempted to update password for user with incorrect password - {request}", request);
            AddError("Incorrect old password");
            return ValidationResult;
        }

        //update user password hash and salt
        var hashAndSalt = _userRepository.HashPassword(request.Password);
        user.Salt = hashAndSalt.Item2;
        user.PasswordHash = hashAndSalt.Item1;

        //pass to domain
        user.AddDomainEvent(new UserPasswordUpdatedEvent(request.Id, request.Email));

        //pass to repository
        _userRepository.Update(user);

        //commit unit of work
        return await Commit(_userRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //check that user exists
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            //log warning
            _logger.LogWarning("Update user role attempt for non-existent user - {request}", request);
            AddError("User not found");
            return ValidationResult;
        }

        //get user from repository by id
        var userToUpdate = await _userRepository.GetByIdAsync(request.Id);

        userToUpdate.Role = request.Role;

        //pass to domain


        //pass to repository
        _userRepository.Update(userToUpdate);

        //commit unit of work
        return await Commit(_userRepository.UnitOfWork);
    }
}