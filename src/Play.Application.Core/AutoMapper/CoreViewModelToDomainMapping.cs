using AutoMapper;
using Play.Application.Core.ViewModels;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Models;

namespace Play.Application.Core.AutoMapper;

public class CoreViewModelToDomainMapping
{
    public static void RegisterMappings(Profile profile)
    {
        //User
        profile.CreateMap<UserViewModel, RegisterUserCommand>()
            .ConstructUsing(c => new RegisterUserCommand(c.Email, c.Password));

        profile.CreateMap<UpdateUserViewModel, UpdateUserCommand>()
            .ConstructUsing(c => new UpdateUserCommand(c.Id, c.Email, c.Password));
        profile.CreateMap<UpdatePasswordUserViewModel, UpdateUserPasswordCommand>()
            .ConstructUsing(c => new UpdateUserPasswordCommand(c.Id, c.Email, c.Password, c.Old_Password));

        profile.CreateMap<UserForgotPasswordViewModel, ForgotPasswordCommand>()
            .ConstructUsing(c => new ForgotPasswordCommand(c.Email));

        profile.CreateMap<UpdateRoleUserViewModel, UpdateUserRoleCommand>()
            .ConstructUsing(c => new UpdateUserRoleCommand(c.Id, c.Email, c.Role));


        // User Profile
        profile.CreateMap<UserProfileViewModel, UpdateUserProfileCommand>()
            .ConstructUsing(c =>
                new UpdateUserProfileCommand(c.Id, c.User_Id, c.FirstName, c.LastName, c.DateOfBirth, c.CompanyName,
                    c.LanguagePreference, c.ThemePreference, c.TIN));

        profile.CreateMap<UserProfileViewModel, RegisterUserProfileCommand>()
            .ConstructUsing(c =>
                new RegisterUserProfileCommand(c.User_Id, c.FirstName, c.LastName, c.DateOfBirth, c.CompanyName,
                    c.ThemePreference, c.TIN));

        profile.CreateMap<UserProfileViewModel, RemoveUserProfileCommand>()
            .ConstructUsing(c => new RemoveUserProfileCommand(c.Id));

        //Email Template
        profile.CreateMap<EmailTemplateViewModel, RegisterNewEmailTemplateCommand>()
            .ConstructUsing(c =>
                new RegisterNewEmailTemplateCommand(new EmailTemplate(Guid.NewGuid(), c.Name, c.Subject, c.Body)));
        profile.CreateMap<UpdateEmailTemplateViewModel, UpdateEmailTemplateCommand>()
            .ConstructUsing(c => new UpdateEmailTemplateCommand(new EmailTemplate(c.Id, c.Name, c.Subject, c.Body)));
    }
}