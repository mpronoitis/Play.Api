using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Core.Interfaces;

namespace Play.Domain.Core.Commands;

public class EmailTemplateCommandHandler : CommandHandler,
    IRequestHandler<RegisterNewEmailTemplateCommand, ValidationResult>,
    IRequestHandler<UpdateEmailTemplateCommand, ValidationResult>,
    IRequestHandler<RemoveEmailTemplateCommand, ValidationResult>
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;

    public EmailTemplateCommandHandler(IEmailTemplateRepository emailTemplateRepository)
    {
        _emailTemplateRepository = emailTemplateRepository;
    }

    public async Task<ValidationResult> Handle(RegisterNewEmailTemplateCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _emailTemplateRepository.AddAsync(request.EmailTemplate);
            return await Commit(_emailTemplateRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(RemoveEmailTemplateCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            //find the email template by id
            var emailTemplate = await _emailTemplateRepository.FindByIdAsync(request.Id);
            if (emailTemplate == null)
            {
                AddError("Email template not found");
                return ValidationResult;
            }

            //delete the email template
            _emailTemplateRepository.Delete(emailTemplate);
            return await Commit(_emailTemplateRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(UpdateEmailTemplateCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            _emailTemplateRepository.Update(request.EmailTemplate);
            return await Commit(_emailTemplateRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }
}