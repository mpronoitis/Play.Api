namespace Play.Services.Api.Controllers.Core;

[Route("emailTemplates")]
public class EmailTemplateController : ApiController
{
    private readonly IEmailTemplateService _emailTemplateService;

    public EmailTemplateController(IEmailTemplateService emailTemplateService)
    {
        _emailTemplateService = emailTemplateService;
    }

    /// <summary>
    ///     Get all templates with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>List of templates</returns>
    [HttpGet("{page}/{pageSize}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Get(int page, int pageSize)
    {
        var templates = await _emailTemplateService.GetTemplatesAsync(page, pageSize);
        return CustomResponse(templates);
    }

    /// <summary>
    ///     Get by Subject
    /// </summary>
    /// <param name="subject">Subject</param>
    /// <returns>List of templates</returns>
    [HttpGet("subject/{subject}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Get(string subject)
    {
        var templates = await _emailTemplateService.GetTemplatesBySubjectAsync(subject);
        return CustomResponse(templates);
    }

    /// <summary>
    ///     Get by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>List of templates</returns>
    [HttpGet("name/{name}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetByName(string name)
    {
        var templates = await _emailTemplateService.GetTemplatesByNameAsync(name);
        return CustomResponse(templates);
    }

    /// <summary>
    ///     Add new template
    /// </summary>
    /// <param name="EmailTemplate">Template ViewModel</param>
    /// <returns>Validation result</returns>
    [HttpPost]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Post([FromBody] EmailTemplateViewModel EmailTemplate)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var result = await _emailTemplateService.AddTemplateAsync(EmailTemplate);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Update template
    /// </summary>
    /// <param name="EmailTemplate">Template ViewModel</param>
    /// <returns>Validation result</returns>
    [HttpPut]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Put([FromBody] UpdateEmailTemplateViewModel EmailTemplate)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var result = await _emailTemplateService.UpdateTemplateAsync(EmailTemplate);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Delete template
    /// </summary>
    /// <param name="id">Template id</param>
    /// <returns>Validation result</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _emailTemplateService.DeleteTemplateAsync(id);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Send test email to specified email address
    /// </summary>
    /// <param name="email">Email address</param>
    /// <param name="templateId">Template id</param>
    /// <returns>Validation result</returns>
    [HttpGet("sendTestEmail/{email}/{templateId}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> SendTestEmail(string email, Guid templateId)
    {
        var result = await _emailTemplateService.SendTestEmailAsync(email, templateId);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Get a template by id
    /// </summary>
    /// <param name="id">Template id</param>
    /// <returns>Template</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Get(Guid id)
    {
        var template = await _emailTemplateService.GetTemplateByIdAsync(id);
        return CustomResponse(template);
    }
}