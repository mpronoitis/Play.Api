using System.Text.RegularExpressions;
using FluentValidation;

namespace Play.Domain.Epp.Commands.Validations;

public class EppDomainValidation<T> : AbstractValidator<T> where T : EppDomainCommand
{
    protected void validateDomain()
    {
        RuleFor(c => c.RegisterDomainModel.DomainName)
            //must end in .gr
            .Must(domainName => domainName.EndsWith(".gr")).WithMessage("Domain name must end in .gr")
            //must be 3-63 characters long
            .Length(3, 63).WithMessage("Domain name must be between 3 and 63 characters long")
            //must not contain invalid characters, allowed are a-z, 0-9, - and .
            .Must(domainName => Regex.IsMatch(domainName, @"^[a-z0-9-\.]+$"))
            .WithMessage("Domain name contains invalid characters")
            .WithMessage("Domain name must not contain invalid characters");
    }

    protected void validateRegistrant()
    {
        RuleFor(c => c.RegisterDomainModel.Registrant)
            //must start with b68_
            .Must(registrant => registrant.StartsWith("b68_")).WithMessage("Registrant must start with b68_")
            //must be 7-63 characters long
            .Length(7, 63).WithMessage("Registrant must be between 7 and 63 characters long")
            //must not contain invalid characters
            .Must(registrant => !registrant.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            .WithMessage("Registrant must not contain invalid characters");
    }

    protected void validateAdmin()
    {
        RuleFor(c => c.RegisterDomainModel.Admin)
            //must start with b68_
            .Must(admin => admin.StartsWith("b68_")).WithMessage("Admin must start with b68_")
            //must be 7-63 characters long
            .Length(7, 63).WithMessage("Admin must be between 7 and 63 characters long")
            //must not contain invalid characters
            .Must(admin => !admin.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            .WithMessage("Admin must not contain invalid characters");
    }

    protected void validateTech()
    {
        RuleFor(c => c.RegisterDomainModel.Tech)
            //must start with b68_
            .Must(tech => tech.StartsWith("b68_")).WithMessage("Tech must start with b68_")
            //must be 7-63 characters long
            .Length(7, 63).WithMessage("Tech must be between 7 and 63 characters long")
            //must not contain invalid characters
            .Must(tech => !tech.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            .WithMessage("Tech must not contain invalid characters");
    }

    protected void validateBilling()
    {
        RuleFor(c => c.RegisterDomainModel.Billing)
            //must start with b68_
            .Must(billing => billing.StartsWith("b68_")).WithMessage("Billing must start with b68_")
            //must be 7-63 characters long
            .Length(7, 63).WithMessage("Billing must be between 7 and 63 characters long")
            //must not contain invalid characters
            .Must(billing => !billing.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            .WithMessage("Billing must not contain invalid characters");
    }

    protected void validatePassword()
    {
        RuleFor(c => c.RegisterDomainModel.Password)
            //Allowed length [8-16]. Allowed chars from groups a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\/?[]. At least on character from each group
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*(){}:;-_+=\/?[\]]).{8,16}$")
            .WithMessage(
                "Password must be between 8 and 16 characters long and contain at least one character from each of the following groups: a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\\/?[].");
    }

    protected void validatePeriod()
    {
        RuleFor(c => c.RegisterDomainModel.Period)
            //must be at least 2 years and at most 10 years
            //allow 2-4-6-8-10 years
            .Must(period => period is 2 or 4 or 6 or 8 or 10)
            .WithMessage("Period must be at least 2 years and at most 10 years");
    }

    protected void validateTransferDomain()
    {
        RuleFor(c => c.TransferDomainModel.DomainName)
            //must end in .gr
            .Must(domainName => domainName.EndsWith(".gr")).WithMessage("Domain name must end in .gr")
            //must be 3-63 characters long
            .Length(3, 63).WithMessage("Domain name must be between 3 and 63 characters long")
            //must not contain invalid characters, allowed are a-z, 0-9, - and .
            .Must(domainName => Regex.IsMatch(domainName, @"^[a-z0-9-\.]+$"))
            .WithMessage("Domain name contains invalid characters")
            .WithMessage("Domain name must not contain invalid characters");
    }

    protected void validateTransferPassword()
    {
        RuleFor(c => c.TransferDomainModel.Password)
            //Allowed length [8-16]. Allowed chars from groups a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\/?[]. At least on character from each group
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*(){}:;-_+=\/?[\]]).{8,16}$")
            .WithMessage(
                "Password must be between 8 and 16 characters long and contain at least one character from each of the following groups: a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\\/?[].");
    }

    protected void validateTransferNewPassword()
    {
        RuleFor(c => c.TransferDomainModel.NewPassword)
            //Allowed length [8-16]. Allowed chars from groups a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\/?[]. At least on character from each group
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*(){}:;-_+=\/?[\]]).{8,16}$")
            .WithMessage(
                "Password must be between 8 and 16 characters long and contain at least one character from each of the following groups: a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\\/?[].");
    }

    protected void ValidateTransferNewContact()
    {
        RuleFor(c => c.TransferDomainModel.ContactId)
            //must start with b68_
            .Must(contactId => contactId.StartsWith("b68_")).WithMessage("Contact ID must start with b68_")
            //must be 7-16
            .Length(7, 16).WithMessage("Contact ID must be between 7 and 16 characters long")
            //must not contain invalid characters
            .Must(contactId => !contactId.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            .WithMessage("Contact ID must not contain invalid characters");
    }

    protected void ValidateRenewDomain()
    {
        RuleFor(c => c.RenewDomainModel.DomainName)
            //must end in .gr
            .Must(domainName => domainName.EndsWith(".gr")).WithMessage("Domain name must end in .gr")
            //must be 3-63 characters long
            .Length(3, 63).WithMessage("Domain name must be between 3 and 63 characters long")
            //must not contain invalid characters, allowed are a-z, 0-9, - and .
            .Must(domainName => Regex.IsMatch(domainName, @"^[a-z0-9-\.]+$"))
            .WithMessage("Domain name contains invalid characters")
            .WithMessage("Domain name must not contain invalid characters");
    }

    protected void ValidateRenewYears()
    {
        RuleFor(c => c.RenewDomainModel.Years)
            //must be at least 2 years and at most 10 years
            //allow 2-4-6-8-10 years
            .Must(years => years is 2 or 4 or 6 or 8 or 10)
            .WithMessage("Years must be at least 2 years and at most 10 years");
    }
}

public class RegisterEppDomainCommandValidation : EppDomainValidation<RegisterEppDomainCommand>
{
    public RegisterEppDomainCommandValidation()
    {
        validateDomain();
        validateRegistrant();
        validateAdmin();
        validateTech();
        validateBilling();
        validatePassword();
        validatePeriod();
    }
}

public class TransferEppDomainCommandValidation : EppDomainValidation<TransferEppDomainCommand>
{
    public TransferEppDomainCommandValidation()
    {
        validateTransferDomain();
        validateTransferPassword();
        validateTransferNewPassword();
        ValidateTransferNewContact();
    }
}

public class RenewEppDomainCommandValidation : EppDomainValidation<RenewEppDomainCommand>
{
    public RenewEppDomainCommandValidation()
    {
        ValidateRenewDomain();
        ValidateRenewYears();
    }
}