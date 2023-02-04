using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using Play.Domain.Edi.Events;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Commands;

public class EdiDocumentCommandHandler : CommandHandler,
    IRequestHandler<RegisterEdiDocumentCommand, ValidationResult>,
    IRequestHandler<UpdateEdiDocumentCommand, ValidationResult>,
    IRequestHandler<RemoveEdiDocumentCommand, ValidationResult>,
    IRequestHandler<ReceivedEdiDocumentCommand, ValidationResult>
{
    private readonly IEdiDocumentRepository _ediDocumentRepository;
    private readonly ILogger<EdiDocumentCommandHandler> _logger;


    public EdiDocumentCommandHandler(IEdiDocumentRepository ediDocumentRepository,
        ILogger<EdiDocumentCommandHandler> logger)
    {
        _ediDocumentRepository = ediDocumentRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Received document from the document reeiver
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ValidationResult> Handle(ReceivedEdiDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return ValidationResult;
        try
        {
            //attempt to decode the payload from base64
            var docPayload = Encoding.UTF8.GetString(Convert.FromBase64String(request.DocumentPayload));

            //decode title from base64 (contains greek characters)
            var title = Encoding.UTF8.GetString(Convert.FromBase64String(request.Title));

            //create new instance of entity
            var entity = new EdiDocument(Guid.NewGuid(), request.Customer_Id, title, request.EdiPayload,
                docPayload, request.Hedentid, false, false, DateTime.Now);
            
            //if customer is unilog (id =bacbf7b1-990d-4918-a864-ff12bd1a6b9e) then set the document as processed and set edi payload
            if (request.Customer_Id == Guid.Parse("bacbf7b1-990d-4918-a864-ff12bd1a6b9e"))
            {
                entity.IsProcessed = true;
                entity.EdiPayload = docPayload;
            }
            
            //save to repository
            _ediDocumentRepository.Register(entity);
            // //pass to domain
            entity.AddDomainEvent(new EdiDocumentReceivedEvent(entity.Id, entity.Customer_Id, title,
                entity.EdiPayload, docPayload, entity.Hedentid, entity.IsProcessed, DateTime.Now));
            //commit unit of work
            return await Commit(_ediDocumentRepository.UnitOfWork);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while handling ReceivedEdiDocumentCommand for request {request}", request);
            AddError(e.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(RegisterEdiDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //create new instance of entity
        var entity = new EdiDocument(Guid.NewGuid(), request.Customer_Id, request.Title, request.EdiPayload,
            request.DocumentPayload, request.Hedentid, request.IsProcessed, request.IsSent, DateTime.Now);
        //pass to domain
        entity.AddDomainEvent(new EdiDocumentRegisteredEvent(entity.Id, entity.Customer_Id, entity.Title,
            entity.EdiPayload, entity.DocumentPayload, entity.Hedentid, entity.IsProcessed, DateTime.Now));
        //save to repository
        _ediDocumentRepository.Register(entity);
        //commit unit of work
        return await Commit(_ediDocumentRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return ValidationResult;
        //get entity from repository
        var entity = await _ediDocumentRepository.GetByIdAsync(request.Id);
        //check that entity exists
        if (entity == null)
        {
            AddError($"Document with hedentid : {request.Hedentid} not found");
            return ValidationResult;
        }

        //pass to domain
        entity.AddDomainEvent(new EdiDocumentRemovedEvent(entity.Id));
        //save to repository
        _ediDocumentRepository.Remove(entity);
        //commit unit of work
        return await Commit(_ediDocumentRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return ValidationResult;
        //get entity from repository
        var entity = await _ediDocumentRepository.GetByIdAsync(request.Id);
        //check that entity exists
        if (entity == null)
        {
            AddError($"Document with hedentid : {request.Hedentid} not found");
            return ValidationResult;
        }

        entity.Hedentid = request.Hedentid;
        entity.Title = request.Title;
        entity.Customer_Id = request.Customer_Id;
        entity.DocumentPayload = request.DocumentPayload;
        entity.EdiPayload = request.EdiPayload;
        entity.IsProcessed = request.IsProcessed;
        //pass to domain
        entity.AddDomainEvent(new EdiDocumentUpdatedEvent(entity.Id, entity.Customer_Id, entity.Title,
            entity.EdiPayload, entity.DocumentPayload, entity.Hedentid, entity.IsProcessed));
        //save to repository
        _ediDocumentRepository.Update(entity);
        //commit unit of work
        return await Commit(_ediDocumentRepository.UnitOfWork);
    }
}