using System;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

/// <summary>
///     Validator for all commands used in <see cref="EdiConnectionCommandHandler" />
/// </summary>
/// <typeparam name="T">
///     <see cref="EdiConnectionCommand" />
/// </typeparam>
public class EdiConnectionValidation<T> : AbstractValidator<T> where T : EdiConnectionCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid Id");
    }

    protected void ValidateModelId()
    {
        RuleFor(c => c.Model_Id)
            .NotEmpty().WithMessage("ModelId is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid ModelId");
    }

    protected void ValidateOrgId()
    {
        RuleFor(c => c.Org_Id)
            .NotEmpty().WithMessage("OrgId is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid OrgId");
    }

    protected void ValidateProfileId()
    {
        RuleFor(c => c.Profile_Id)
            .NotEmpty().WithMessage("ProfileId is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid ProfileId");
    }

    protected void ValidateFtpHostname()
    {
        RuleFor(c => c.Ftp_Hostname)
            .NotEmpty().WithMessage("FtpHostname is required")
            .MaximumLength(100).WithMessage("FtpHostname must be 100 characters or less")
            .MinimumLength(3).WithMessage("FtpHostname must be 3 characters or more")
            .Matches(@"^[a-zA-Z0-9-_.]+$")
            .WithMessage("FtpHostname can only contain letters, numbers, hyphens and underscores");
    }

    protected void ValidateFtpUsername()
    {
        RuleFor(c => c.Ftp_Username)
            .NotEmpty().WithMessage("FtpUsername is required")
            .MaximumLength(100).WithMessage("FtpUsername must be 100 characters or less")
            .MinimumLength(3).WithMessage("FtpUsername must be 3 characters or more");
    }

    protected void ValidateFtpPassword()
    {
        RuleFor(c => c.Ftp_Password)
            .NotEmpty().WithMessage("FtpPassword is required")
            .MaximumLength(100).WithMessage("FtpPassword must be 100 characters or less")
            .MinimumLength(3).WithMessage("FtpPassword must be 3 characters or more");
    }
}

/// <summary>
///     Validate RegisterEdiConnectionCommand
/// </summary>
public class RegisterEdiConnectionCommandValidation : EdiConnectionValidation<RegisterEdiConnectionCommand>
{
    public RegisterEdiConnectionCommandValidation()
    {
        ValidateModelId();
        ValidateOrgId();
        ValidateProfileId();
        ValidateFtpHostname();
        ValidateFtpUsername();
        ValidateFtpPassword();
    }
}

/// <summary>
///     Validate UpdateEdiConnectionCommand
/// </summary>
public class UpdateEdiConnectionCommandValidation : EdiConnectionValidation<UpdateEdiConnectionCommand>
{
    public UpdateEdiConnectionCommandValidation()
    {
        ValidateId();
        ValidateModelId();
        ValidateOrgId();
        ValidateProfileId();
        ValidateFtpHostname();
        ValidateFtpUsername();
        ValidateFtpPassword();
    }
}

/// <summary>
///     Validate delete command
/// </summary>
public class RemoveEdiConnectionCommandValidation : EdiConnectionValidation<RemoveEdiConnectionCommand>
{
    public RemoveEdiConnectionCommandValidation()
    {
        ValidateId();
    }
}