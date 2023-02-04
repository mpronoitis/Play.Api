using System;
using NetDevPack.Messaging;
using Play.Domain.Core.Commands.Validations;
using Play.Domain.Core.Models;

namespace Play.Domain.Core.Commands;

public class EmailTemplateCommand : Command
{
    public Guid Id { get; set; }
    public EmailTemplate EmailTemplate { get; set; }
}

public class RegisterNewEmailTemplateCommand : EmailTemplateCommand
{
    public RegisterNewEmailTemplateCommand(EmailTemplate emailTemplate)
    {
        EmailTemplate = emailTemplate;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEmailTemplateValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEmailTemplateCommand : EmailTemplateCommand
{
    public UpdateEmailTemplateCommand(EmailTemplate emailTemplate)
    {
        EmailTemplate = emailTemplate;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEmailTemplateValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEmailTemplateCommand : EmailTemplateCommand
{
    public RemoveEmailTemplateCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEmailTemplateValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}