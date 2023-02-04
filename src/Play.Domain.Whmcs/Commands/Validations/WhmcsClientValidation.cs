using FluentValidation;

namespace Play.Domain.Whmcs.Commands.Validations;

public class WhmcsClientValidation<T> : AbstractValidator<T> where T : WhmcsAddClientCommand
{
    protected void validateFirstName()
    {
        RuleFor(c => c.Client.FirstName)
            .NotEmpty().WithMessage("Please ensure you have entered a first name")
            .Length(2, 100).WithMessage("The first name must have between 2 and 100 characters");
    }

    protected void validateLastName()
    {
        RuleFor(c => c.Client.LastName)
            .NotEmpty().WithMessage("Please ensure you have entered a last name")
            .Length(2, 100).WithMessage("The last name must have between 2 and 100 characters");
    }

    protected void validateEmail()
    {
        RuleFor(c => c.Client.Email)
            .NotEmpty().WithMessage("Please ensure you have entered an email address")
            .EmailAddress().WithMessage("Please ensure you have entered a valid email address");
    }

    protected void ValidateAddress()
    {
        RuleFor(c => c.Client.Address1)
            .NotEmpty().WithMessage("Please ensure you have entered an address")
            .Length(2, 100).WithMessage("The address must have between 2 and 100 characters");
    }

    protected void ValidateCity()
    {
        RuleFor(c => c.Client.City)
            .NotEmpty().WithMessage("Please ensure you have entered a city")
            .Length(2, 100).WithMessage("The city must have between 2 and 100 characters");
    }

    protected void ValidateState()
    {
        RuleFor(c => c.Client.State)
            .NotEmpty().WithMessage("Please ensure you have entered a state")
            .Length(2, 100).WithMessage("The state must have between 2 and 100 characters");
    }

    protected void ValidatePostcode()
    {
        RuleFor(c => c.Client.Postcode)
            .NotEmpty().WithMessage("Please ensure you have entered a postcode")
            .Length(2, 100).WithMessage("The postcode must have between 2 and 100 characters");
    }

    protected void ValidateCountryCode()
    {
        RuleFor(c => c.Client.Country)
            .NotEmpty().WithMessage("Please ensure you have entered a country code")
            .Length(2, 2).WithMessage("The country code must have 2 characters");
    }

    protected void ValidatePhoneNumber()
    {
        RuleFor(c => c.Client.PhoneNumber)
            .NotEmpty().WithMessage("Please ensure you have entered a phone number")
            .Length(2, 100).WithMessage("The phone number must have between 2 and 100 characters");
    }

    protected void ValidatePassword()
    {
        RuleFor(c => c.Client.Password)
            .NotEmpty().WithMessage("Please ensure you have entered a password")
            .Length(2, 100).WithMessage("The password must have between 2 and 100 characters");
    }
}

public class AddWhmcsClientCommandValidation : WhmcsClientValidation<WhmcsAddClientCommand>
{
    public AddWhmcsClientCommandValidation()
    {
        validateFirstName();
        validateLastName();
        validateEmail();
        ValidateAddress();
        ValidateCity();
        ValidateState();
        ValidatePostcode();
        ValidateCountryCode();
        ValidatePhoneNumber();
        ValidatePassword();
    }
}