using System;
using NetDevPack.Messaging;
using Play.Domain.Core.Commands.Validations;

namespace Play.Domain.Core.Commands;

public class UserProfileCommand : Command
{
    public Guid Id { get; set; }
    public Guid User_Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CompanyName { get; set; }
    public string LanguagePreference { get; set; }

    public string ThemePreference { get; set; }

    public string TIN { get; set; }
}

public class RegisterUserProfileCommand : UserProfileCommand
{
    public RegisterUserProfileCommand(Guid user_Id, string firstName, string lastName, DateTime dateOfBirth,
        string companyName, string themePreference, string tin)
    {
        User_Id = user_Id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CompanyName = companyName;
        ThemePreference = themePreference;
        TIN = tin;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterUserProfileValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateUserProfileCommand : UserProfileCommand
{
    public UpdateUserProfileCommand(Guid id, Guid user_Id, string firstName, string lastName, DateTime dateOfBirth,
        string companyName, string languagePreference, string themePreference, string tin)
    {
        Id = id;
        User_Id = user_Id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CompanyName = companyName;
        LanguagePreference = languagePreference;
        ThemePreference = themePreference;
        TIN = tin;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateUserProfileValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveUserProfileCommand : UserProfileCommand
{
    public RemoveUserProfileCommand(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveUserProfileValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}