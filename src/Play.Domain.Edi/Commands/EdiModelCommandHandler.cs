using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Edi.Events;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Commands;

public class EdiModelCommandHandler : CommandHandler,
    IRequestHandler<RegisterEdiModelCommand, ValidationResult>,
    IRequestHandler<UpdateEdiModelCommand, ValidationResult>,
    IRequestHandler<RemoveEdiModelCommand, ValidationResult>
{
    private readonly IEdiModelRepository _ediModelRepository;
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;

    /// constructor , inject the repositories
    public EdiModelCommandHandler(IEdiModelRepository ediModelRepository,
        IEdiOrganizationRepository ediOrganizationRepository)
    {
        _ediModelRepository = ediModelRepository;
        _ediOrganizationRepository = ediOrganizationRepository;
    }

    public async Task<ValidationResult> Handle(RegisterEdiModelCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //create new model instance
        var ediModel = new EdiModel(Guid.NewGuid(), request.Org_id, request.Title, request.SegmentTerminator,
            request.SubElementSeperator, request.ElementSeparator, request.Enabled);

        //check that no other model has the same Title
        var existingModel = await _ediModelRepository.GetByTitleAsync(ediModel.Title);
        if (existingModel != null)
        {
            AddError("There is already a model with this Title");
            return ValidationResult;
        }

        //check that org_id belongs to an existing organization
        var existingOrganization = await _ediOrganizationRepository.GetByIdAsync(ediModel.Org_Id);
        if (existingOrganization == null)
        {
            AddError("The organization does not exist");
            return ValidationResult;
        }

        //pass event to domain
        ediModel.AddDomainEvent(new EdiModelRegisteredEvent(ediModel.Id, ediModel.Org_Id, ediModel.Title,
            ediModel.SegmentTerminator,
            ediModel.SubElementSeparator, ediModel.ElementSeparator, ediModel.Enabled));
        //add model to repository
        _ediModelRepository.Add(ediModel);
        //commit unit of work
        return await Commit(_ediModelRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiModelCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //get model from repository
        var ediModel = await _ediModelRepository.GetByIdAsync(request.Id);
        if (ediModel == null)
        {
            AddError("The model does not exist");
            return ValidationResult;
        }

        //pass event to domain
        ediModel.AddDomainEvent(new EdiModelRemovedEvent(ediModel.Id));
        //pass to repo
        _ediModelRepository.Remove(ediModel);
        //commit unit of work
        return await Commit(_ediModelRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiModelCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //get model from repository
        var ediModel = await _ediModelRepository.GetByIdAsync(request.Id);
        if (ediModel == null)
        {
            AddError("The model does not exist");
            return ValidationResult;
        }

        //check that no other model has the same Title
        var existingModel = await _ediModelRepository.GetByTitleAsync(request.Title);
        if (existingModel != null && existingModel.Id != ediModel.Id)
        {
            AddError("There is already a model with this Title");
            return ValidationResult;
        }

        //check that org_id belongs to an existing organization
        var existingOrganization = await _ediOrganizationRepository.GetByIdAsync(request.Org_id);
        if (existingOrganization == null)
        {
            AddError("The organization does not exist");
            return ValidationResult;
        }

        ediModel.Enabled = request.Enabled;
        ediModel.Title = request.Title;
        ediModel.ElementSeparator = request.ElementSeparator;
        ediModel.Org_Id = request.Org_id;
        ediModel.SegmentTerminator = request.SegmentTerminator;
        ediModel.SubElementSeparator = request.SubElementSeperator;
        //update model
        ediModel.AddDomainEvent(new EdiModelUpdatedEvent(ediModel.Id, request.Org_id, request.Title,
            request.SegmentTerminator,
            request.SubElementSeperator, request.ElementSeparator, request.Enabled));
        //pass to repo
        _ediModelRepository.Update(ediModel);
        //commit unit of work
        return await Commit(_ediModelRepository.UnitOfWork);
    }
}