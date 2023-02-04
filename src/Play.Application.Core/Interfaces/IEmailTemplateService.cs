using FluentValidation.Results;
using Play.Application.Core.ViewModels;
using Play.Domain.Core.Models;

namespace Play.Application.Core.Interfaces;

public interface IEmailTemplateService
{
    /// <summary>
    ///     Get all templates with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>List of templates</returns>
    Task<List<EmailTemplate>> GetTemplatesAsync(int page, int pageSize);

    /// <summary>
    ///     Get by Subject
    /// </summary>
    /// <param name="subject">Subject</param>
    /// <returns>List of templates</returns>
    Task<List<EmailTemplate>> GetTemplatesBySubjectAsync(string subject);

    /// <summary>
    ///     Get by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>List of templates</returns>
    Task<List<EmailTemplate>> GetTemplatesByNameAsync(string name);

    /// <summary>
    ///     Add new template
    /// </summary>
    /// <param name="EmailTemplate">Template ViewModel</param>
    /// <returns>Validation result</returns>
    Task<ValidationResult> AddTemplateAsync(EmailTemplateViewModel EmailTemplate);

    /// <summary>
    ///     Update template
    /// </summary>
    /// <param name="EmailTemplate">Template ViewModel</param>
    /// <returns>Validation result</returns>
    Task<ValidationResult> UpdateTemplateAsync(UpdateEmailTemplateViewModel EmailTemplate);

    /// <summary>
    ///     Delete template
    /// </summary>
    /// <param name="id">Template id</param>
    /// <returns>Validation result</returns>
    Task<ValidationResult> DeleteTemplateAsync(Guid id);

    /// <summary>
    ///     Send test email to specified email address
    /// </summary>
    /// <param name="email">Email address</param>
    /// <param name="templateId">Template id</param>
    /// <returns>Validation result</returns>
    Task<ValidationResult> SendTestEmailAsync(string email, Guid templateId);

    /// <summary>
    ///     Get template by id
    /// </summary>
    /// <param name="id">Template id</param>
    /// <returns>Template</returns>
    Task<EmailTemplate> GetTemplateByIdAsync(Guid id);
}
