using FluentValidation.Results;
using Play.Application.Core.ViewModels;

namespace Play.Application.Core.Interfaces;

public interface IUserProfileService
{
    Task<UserProfileViewModel> GetAsync(Guid id);
    Task<UserProfileViewModel> GetByUserId(Guid user_id);
    Task<ValidationResult> Register(UserProfileViewModel userProfileViewModel);
    Task<ValidationResult> Remove(UserProfileViewModel userProfileViewModel);
    Task<ValidationResult> Update(UserProfileViewModel userProfileViewModel);

    Task<IEnumerable<UserProfileViewModel>> GetAll(int page = 1, int pageSize = 10);
}