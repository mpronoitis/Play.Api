using FluentEmail.Core;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using Play.Domain.Core.Interfaces;

namespace Play.Domain.Contact.Commands;

public class ContactRequestCommandHandler : CommandHandler,
    IRequestHandler<RegisterContactRequestCommand, ValidationResult>
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly ILogger<ContactRequestCommandHandler> _logger;
    private readonly IFluentEmail _mailer;

    public ContactRequestCommandHandler(IFluentEmail mailer, IEmailTemplateRepository emailTemplateRepository,
        ILogger<ContactRequestCommandHandler> logger)
    {
        _mailer = mailer;
        _emailTemplateRepository = emailTemplateRepository;
        _logger = logger;
    }

    public async Task<ValidationResult> Handle(RegisterContactRequestCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //get email template with name "Contact Request"
        var emailTemplate = await _emailTemplateRepository.GetByNameAsync("Contact Request");

        //if no template found, log error and return
        if (emailTemplate.Count == 0)
        {
            _logger.LogError("Email template {0} not found", "Contact Request");
            return request.ValidationResult;
        }

        var email = _mailer
            .To("websupport@playsystems.io")
            .Subject("Contact Request 📞")
            .UsingTemplate(emailTemplate[0].Body, new
            {
                request.Subject,
                request.Message,
                request.PhoneNumber,
                request.Email,
                CreatedAt = request.CreatedOn
            });
        try
        {
            await email.SendAsync(cancellationToken);
            return request.ValidationResult;
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return request.ValidationResult;
        }
    }
}