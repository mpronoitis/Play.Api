using FluentValidation;

namespace Play.Domain.Epp.Commands.Validations;

public class EppContactValidation<T> : AbstractValidator<T> where T : EppContactCommand
{
    //validate id , should start with b68_ and can be from 5 to 16 charachters (including b68_)
    protected void ValidateId()
    {
        RuleFor(c => c.EPPContact.Id)
            .NotEmpty().WithMessage("Contact id is required")
            .Length(5, 16).WithMessage("Contact id should be between 5 and 16 characters")
            .Matches(@"^b68_").WithMessage("Contact id should start with b68_");
    }

    //validate loc_Name , should be between 1 and 32 charachters (localized name)
    protected void ValidateLocName()
    {
        RuleFor(c => c.EPPContact.loc_Name)
            .NotEmpty().WithMessage("Contact loc_Name is required")
            .Length(1, 32).WithMessage("Contact loc_Name should be between 1 and 32 characters");
    }

    //validate loc_Org , should be between 1 and 32 charachters (localized organization)
    protected void ValidateLocOrg()
    {
        RuleFor(c => c.EPPContact.loc_Org)
            .NotEmpty().WithMessage("Contact loc_Org is required")
            .Length(1, 32).WithMessage("Contact loc_Org should be between 1 and 32 characters");
    }

    //validate loc_Street , should be between 1 and 32 charachters (localized street)
    protected void ValidateLocStreet()
    {
        RuleFor(c => c.EPPContact.loc_Street)
            .NotEmpty().WithMessage("Contact loc_Street is required")
            .Length(1, 32).WithMessage("Contact loc_Street should be between 1 and 32 characters");
    }

    //validate loc_City , should be between 1 and 32 charachters (localized city)
    protected void ValidateLocCity()
    {
        RuleFor(c => c.EPPContact.loc_City)
            .NotEmpty().WithMessage("Contact loc_City is required")
            .Length(1, 32).WithMessage("Contact loc_City should be between 1 and 32 characters");
    }

    //validate loc_State , should be between 1 and 32 charachters (localized state)
    protected void ValidateLocState()
    {
        RuleFor(c => c.EPPContact.loc_State)
            .NotEmpty().WithMessage("Contact loc_State is required")
            .Length(1, 32).WithMessage("Contact loc_State should be between 1 and 32 characters");
    }

    //validate loc_PostalCode , should be between 1 and 32 charachters (localized postal code)  
    protected void ValidateLocPostalCode()
    {
        RuleFor(c => c.EPPContact.loc_PostalCode)
            .NotEmpty().WithMessage("Contact loc_PostalCode is required")
            .Length(1, 32).WithMessage("Contact loc_PostalCode should be between 1 and 32 characters");
    }

    //validate loc_Country , should be between 1 and 32 charachters (localized country)
    protected void ValidateLocCountry()
    {
        RuleFor(c => c.EPPContact.loc_Country)
            .NotEmpty().WithMessage("Contact loc_Country is required")
            .Length(1, 32).WithMessage("Contact loc_Country should be between 1 and 32 characters");
    }

    //validate int_Name , should be between 1 and 32 charachters (international name)
    protected void ValidateIntName()
    {
        RuleFor(c => c.EPPContact.int_Name)
            .NotEmpty().WithMessage("Contact int_Name is required")
            .Length(1, 32).WithMessage("Contact int_Name should be between 1 and 32 characters");
    }

    //validate int_Org , should be between 1 and 32 charachters (international organization)
    protected void ValidateIntOrg()
    {
        RuleFor(c => c.EPPContact.int_Org)
            .NotEmpty().WithMessage("Contact int_Org is required")
            .Length(1, 32).WithMessage("Contact int_Org should be between 1 and 32 characters");
    }

    //validate int_Street , should be between 1 and 32 charachters (international street)
    protected void ValidateIntStreet()
    {
        RuleFor(c => c.EPPContact.int_Street)
            .NotEmpty().WithMessage("Contact int_Street is required")
            .Length(1, 32).WithMessage("Contact int_Street should be between 1 and 32 characters");
    }

    //validate int_City , should be between 1 and 32 charachters (international city)
    protected void ValidateIntCity()
    {
        RuleFor(c => c.EPPContact.int_City)
            .NotEmpty().WithMessage("Contact int_City is required")
            .Length(1, 32).WithMessage("Contact int_City should be between 1 and 32 characters");
    }

    //validate int_State , should be between 1 and 32 charachters (international state)
    protected void ValidateIntState()
    {
        RuleFor(c => c.EPPContact.int_State)
            .NotEmpty().WithMessage("Contact int_State is required")
            .Length(1, 32).WithMessage("Contact int_State should be between 1 and 32 characters");
    }

    //validate int_PostalCode , should be between 1 and 32 charachters (international postal code)
    protected void ValidateIntPostalCode()
    {
        RuleFor(c => c.EPPContact.int_PostalCode)
            .NotEmpty().WithMessage("Contact int_PostalCode is required")
            .Length(1, 32).WithMessage("Contact int_PostalCode should be between 1 and 32 characters");
    }

    //validate int_Country , should be between 1 and 32 charachters (international country)
    protected void ValidateIntCountry()
    {
        RuleFor(c => c.EPPContact.int_Country)
            .NotEmpty().WithMessage("Contact int_Country is required")
            .Length(1, 32).WithMessage("Contact int_Country should be between 1 and 32 characters");
    }

    // The phone / fax number must be a valid international number
    // language based on the E.164 standard and format
    // +CCC.NNNNNNNNNNNN, where CCC is the country code (1-3 digits) and
    //     N the telephone number (up to 14 digits) [E164a].
    // If CCC = 1 digit, then the maximum number of digits for N = 14.
    // If CCC = 2 digits, then the maximum number of digits for N = 13.
    // If CCC = 3 digits, then the maximum number of digits for N = 12.
    protected void ValidateVoice()
    {
        RuleFor(c => c.EPPContact.Voice)
            .NotEmpty().WithMessage("Contact phone is required")
            .Matches(@"^\+[0-9]{1,3}\.[0-9]{1,14}$")
            .WithMessage("Contact phone should be a valid international number");
    }

    //fax
    protected void ValidateFax()
    {
        RuleFor(c => c.EPPContact.Fax)
            .NotEmpty().WithMessage("Contact fax is required")
            .Matches(@"^\+[0-9]{1,3}\.[0-9]{1,14}$").WithMessage("Contact fax should be a valid international number");
    }

    //validate email
    protected void ValidateEmail()
    {
        RuleFor(c => c.EPPContact.Email)
            .NotEmpty().WithMessage("Contact email is required")
            .EmailAddress().WithMessage("Contact email should be a valid email address");
    }

    //validate password
    protected void ValidatePassword()
    {
        RuleFor(c => c.EPPContact.Password)
            //Allowed length [8-16]. Allowed chars from groups a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\/?[]. At least on character from each group
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*(){}:;-_+=\/?[\]]).{8,16}$")
            .WithMessage(
                "Password must be between 8 and 16 characters long and contain at least one character from each of the following groups: a-z, A-Z, 0-9, ~!@#$%^&*(){}:;-_+=\\/?[].");
    }

    //disclose name
    protected void ValidateDiscloseName()
    {
        RuleFor(c => c.EPPContact.DiscloseFlag)
            .NotEmpty().WithMessage("Contact disclose name is required")
            .Must(discloseName => discloseName == "0" || discloseName == "1")
            .WithMessage("Contact disclose name should be 0 or 1");
    }
}

public class RegisterEppContactCommandValidation : EppContactValidation<RegisterEppContactCommand>
{
    public RegisterEppContactCommandValidation()
    {
        ValidateId();
        ValidateLocName();
        ValidateLocOrg();
        ValidateLocStreet();
        ValidateLocCity();
        ValidateLocState();
        ValidateLocPostalCode();
        ValidateLocCountry();
        ValidateIntName();
        ValidateIntOrg();
        ValidateIntStreet();
        ValidateIntCity();
        ValidateIntState();
        ValidateIntPostalCode();
        ValidateIntCountry();
        ValidateVoice();
        ValidateEmail();
        ValidatePassword();
        ValidateDiscloseName();
        ValidateFax();
    }
}

public class UpdateEppContactCommandValidation : EppContactValidation<UpdateEppContactCommand>
{
    public UpdateEppContactCommandValidation()
    {
        ValidateId();
        ValidateLocName();
        ValidateLocOrg();
        ValidateLocStreet();
        ValidateLocCity();
        ValidateLocState();
        ValidateLocPostalCode();
        ValidateLocCountry();
        ValidateIntName();
        ValidateIntOrg();
        ValidateIntStreet();
        ValidateIntCity();
        ValidateIntState();
        ValidateIntPostalCode();
        ValidateIntCountry();
        ValidateVoice();
        ValidateEmail();
        ValidateDiscloseName();
        ValidateFax();
    }
}