using FluentValidation.Results;
using Play.Application.Core.ViewModels;
using Play.Domain.Core.Models;

namespace Play.Application.Core.Interfaces;

public interface IUserService
{
    Task<UserViewModel> GetAsync(Guid id);
    Task<ValidationResult> Update(UpdateUserViewModel userViewModel);
    Task<ValidationResult> UpdatePassword(UpdatePasswordUserViewModel userViewModel);
    Task<ValidationResult> Register(UserViewModel userViewModel);

    Task<ValidationResult> UpdateRole(UpdateRoleUserViewModel userViewModel);
    Task<ValidationResult> Remove(Guid id);

    /// <summary>
    ///     Login User with email and password
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns>Tuple with result and last login date</returns>
    Task<(bool, DateTime)> LoginAsync(UserViewModel userViewModel);
    /// <summary>
    ///     Login WHMCS User with email and password
    /// </summary>
    /// <param name="userViewModel"></param>
    /// <returns>Tuple with result and last login date</returns>
    Task<(bool, DateTime)> LoginBotAsync(UserViewModel userViewModel);

    Task<User> GetUserByEmailAsync(string email);
    Task<ValidationResult> ForgotPassword(UserForgotPasswordViewModel userViewModel);

    /// <summary>
    ///     Get All Users from Database with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    Task<IEnumerable<User>> GetAllAsync(int page, int pageSize);

    /// <summary>
    ///     Get total count of users
    /// </summary>
    /// <returns></returns>
    Task<int> GetTotalCountAsync();

    /// <summary>
    ///     Get total count of users with a given role
    ///     Optionally pass a time range to get count of users with a given role with CreatedAt in that range
    /// </summary>
    /// <param name="role"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    Task<int> GetTotalCountByRoleAsync(string role, DateTime? from = null, DateTime? to = null);

    /// <summary>
    ///     Get count of users created within a given time range
    /// </summary>
    /// <param name="startDateTime"></param>
    /// <param name="endDateTime"></param>
    /// <returns></returns>
    Task<int> GetCountByCreatedAtAsync(DateTime startDateTime, DateTime endDateTime);
}