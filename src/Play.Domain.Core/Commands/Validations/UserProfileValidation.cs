using System;
using FluentValidation;

namespace Play.Domain.Core.Commands.Validations;

public class UserProfileValidation<T> : AbstractValidator<T> where T : UserProfileCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required")
            .NotEmpty().WithMessage("Id is required");
    }

    protected void ValidateUserId()
    {
        RuleFor(c => c.User_Id)
            .NotEqual(Guid.Empty).WithMessage("UserId is required")
            .NotEmpty().WithMessage("UserId is required");
    }

    protected void ValidateFirstName()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .Length(2, 100).WithMessage("FirstName must have between 2 and 100 characters");
    }

    protected void ValidateLastName()
    {
        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Length(2, 100).WithMessage("LastName must have between 2 and 100 characters");
    }

    protected void ValidateDateOfBirth()
    {
        RuleFor(c => c.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth is required")
            .Must(BeAValidDate).WithMessage("DateOfBirth is not a valid date");
    }

    protected void ValidateCompanyName()
    {
        RuleFor(c => c.CompanyName)
            .NotEmpty().WithMessage("CompanyName is required")
            .Length(2, 100).WithMessage("CompanyName must have between 2 and 100 characters");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date <= DateTime.Now;
    }
}

public class RegisterUserProfileValidation : UserProfileValidation<RegisterUserProfileCommand>
{
    public RegisterUserProfileValidation()
    {
        ValidateUserId();
        ValidateFirstName();
        ValidateLastName();
        ValidateDateOfBirth();
        ValidateCompanyName();
    }
}

public class UpdateUserProfileValidation : UserProfileValidation<UpdateUserProfileCommand>
{
    public UpdateUserProfileValidation()
    {
        ValidateId();
        ValidateFirstName();
        ValidateLastName();
        ValidateDateOfBirth();
        ValidateCompanyName();
    }
}

public class RemoveUserProfileValidation : UserProfileValidation<RemoveUserProfileCommand>
{
    public RemoveUserProfileValidation()
    {
        ValidateId();
    }
}