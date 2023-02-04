using AutoMapper;
using FluentEmail.Core;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Core.Interfaces;
using Play.Application.Core.ViewModels;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;

namespace Play.Application.Core.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IFluentEmail _fluentEmail;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EmailTemplateService(IMapper mapper, IMediatorHandler mediatorHandler,
        IEmailTemplateRepository emailTemplateRepository, IFluentEmail fluentEmail)
    {
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _emailTemplateRepository = emailTemplateRepository;
        _fluentEmail = fluentEmail;
    }

    /// <summary>
    ///     Get all templates with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>List of templates</returns>
    public async Task<List<EmailTemplate>> GetTemplatesAsync(int page, int pageSize)
    {
        return await _emailTemplateRepository.GetAllAsync(page, pageSize);
    }

    /// <summary>
    ///     Get by Subject
    /// </summary>
    /// <param name="subject">Subject</param>
    /// <returns>List of templates</returns>
    public async Task<List<EmailTemplate>> GetTemplatesBySubjectAsync(string subject)
    {
        return await _emailTemplateRepository.GetBySubjectAsync(subject);
    }

    /// <summary>
    ///     Get by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>List of templates</returns>
    public async Task<List<EmailTemplate>> GetTemplatesByNameAsync(string name)
    {
        return await _emailTemplateRepository.GetByNameAsync(name);
    }

    /// <summary>
    ///     Add new template
    /// </summary>
    /// <param name="EmailTemplate">Template ViewModel</param>
    /// <returns>Validation result</returns>
    public async Task<ValidationResult> AddTemplateAsync(EmailTemplateViewModel EmailTemplate)
    {
        var command = _mapper.Map<RegisterNewEmailTemplateCommand>(EmailTemplate);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     Update template
    /// </summary>
    /// <param name="EmailTemplate">Template ViewModel</param>
    /// <returns>Validation result</returns>
    public async Task<ValidationResult> UpdateTemplateAsync(UpdateEmailTemplateViewModel EmailTemplate)
    {
        var command = _mapper.Map<UpdateEmailTemplateCommand>(EmailTemplate);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     Delete template
    /// </summary>
    /// <param name="id">Template id</param>
    /// <returns>Validation result</returns>
    public async Task<ValidationResult> DeleteTemplateAsync(Guid id)
    {
        var command = new RemoveEmailTemplateCommand(id);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     Send test email to specified email address
    /// </summary>
    /// <param name="email">Email address</param>
    /// <param name="templateId">Template id</param>
    /// <returns>Validation result</returns>
    public async Task<ValidationResult> SendTestEmailAsync(string email, Guid templateId)
    {
        var template = await _emailTemplateRepository.FindByIdAsync(templateId);
        if (template == null)
            return new ValidationResult(new List<ValidationFailure>
            {
                new("Template", "Template not found")
            });

        var emailMessage = _fluentEmail
            .To(email)
            .Subject(template.Subject)
            .UsingTemplate(template.Body, new
            {
                Username = "foo@example.com",
                Email = "foo@example.com",
                Password = "123456",
                Otp = "123456",
                Firstname = "John",
                Lastname = "Doe",
                Company = "Foo Inc.",
                Address = "123 Foo St.",
                City = "Foo City",
                State = "Foo State",
                Zip = "12345",
                Country = "Foo Country",
                Phone = "123-456-7890",
                Url = "https://example.com",
                Date = DateTime.Now.ToString("MM/dd/yyyy"),
                Time = DateTime.Now.ToString("hh:mm tt")
            });


        await emailMessage.SendAsync();

        return new ValidationResult();
    }

    /// <summary>
    ///     Get template by id
    /// </summary>
    /// <param name="id">Template id</param>
    /// <returns>Template</returns>
    public async Task<EmailTemplate> GetTemplateByIdAsync(Guid id)
    {
        return await _emailTemplateRepository.FindByIdAsync(id);
    }
}