using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Core.Events;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;

namespace Play.Domain.Core.Commands;

public class UserProfileCommandHandler : CommandHandler, IRequestHandler<RegisterUserProfileCommand, ValidationResult>,
    IRequestHandler<UpdateUserProfileCommand, ValidationResult>,
    IRequestHandler<RemoveUserProfileCommand, ValidationResult>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfileCommandHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<ValidationResult> Handle(RegisterUserProfileCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        var newUserProfile = new UserProfile(Guid.NewGuid(), request.User_Id, request.FirstName, request.LastName,
            request.DateOfBirth, request.CompanyName, "en", request.ThemePreference, request.TIN);


        // newUserProfile.AddDomainEvent(new UserProfileUpdatedEvent(newUserProfile.Id, newUserProfile.User_Id, newUserProfile.FirstName, newUserProfile.LastName, newUserProfile.DateOfBirth, newUserProfile.CompanyName));

        _userProfileRepository.Add(newUserProfile);
        //commit unit of work
        return await Commit(_userProfileRepository.UnitOfWork);
    }


    public async Task<ValidationResult> Handle(RemoveUserProfileCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        // get userProfile to remove
        var userProfileToRemove = await _userProfileRepository.GetByIdAsync(request.Id);

        if (userProfileToRemove == null)
        {
            AddError("The userProfile does not exist");
            return ValidationResult;
        }

        _userProfileRepository.Remove(userProfileToRemove);

        //commit unit of work
        return await Commit(_userProfileRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        // get userProfile to remove
        var userProfileToUpdate = await _userProfileRepository.GetByIdAsync(request.Id);
        //update fields
        userProfileToUpdate.CompanyName = request.CompanyName;
        userProfileToUpdate.DateOfBirth = request.DateOfBirth;
        userProfileToUpdate.FirstName = request.FirstName;
        userProfileToUpdate.LastName = request.LastName;
        userProfileToUpdate.ThemePreference = request.ThemePreference;
        userProfileToUpdate.TIN = request.TIN;
        userProfileToUpdate.LanguagePreference = request.LanguagePreference;

        userProfileToUpdate.AddDomainEvent(new UserProfileUpdatedEvent(userProfileToUpdate.Id,
            userProfileToUpdate.User_Id, userProfileToUpdate.FirstName, userProfileToUpdate.LastName,
            userProfileToUpdate.DateOfBirth, userProfileToUpdate.CompanyName));

        _userProfileRepository.Update(userProfileToUpdate);

        //commit unit of work
        return await Commit(_userProfileRepository.UnitOfWork);
    }
}