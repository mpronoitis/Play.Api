using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Core.Models;

namespace Play.Domain.Core.Interfaces;

public interface IEmailTemplateRepository
{
    public IUnitOfWork UnitOfWork { get; }

    /// <summary>
    ///     Find by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<EmailTemplate> FindByIdAsync(Guid id);

    /// <summary>
    ///     Get all templates with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>List of templates</returns>
    Task<List<EmailTemplate>> GetAllAsync(int page, int pageSize);

    /// <summary>
    ///     Get by Subject
    /// </summary>
    /// <param name="subject">Subject</param>
    /// <returns>List of templates</returns>
    Task<List<EmailTemplate>> GetBySubjectAsync(string subject);

    /// <summary>
    ///     Get by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>List of templates</returns>
    Task<List<EmailTemplate>> GetByNameAsync(string name);

    /// <summary>
    ///     Add new template
    /// </summary>
    /// <param name="template">Template</param>
    /// <returns> void </returns>
    Task AddAsync(EmailTemplate template);

    /// <summary>
    ///     Update template
    /// </summary>
    /// <param name="template">Template</param>
    /// <returns> void </returns>
    void Update(EmailTemplate template);

    /// <summary>
    ///     Delete template
    /// </summary>
    /// <param name="template">Template</param>
    /// <returns> void </returns>
    void Delete(EmailTemplate template);

    void Dispose();
}