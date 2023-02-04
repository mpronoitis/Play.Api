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

public class EdiSegmentCommandHandler : CommandHandler,
    IRequestHandler<RegisterEdiSegmentCommand, ValidationResult>,
    IRequestHandler<UpdateEdiSegmentCommand, ValidationResult>,
    IRequestHandler<RemoveEdiSegmentCommand, ValidationResult>
{
    private readonly IEdiModelRepository _ediModelRepository;
    private readonly IEdiSegmentRepository _ediSegmentRepository;

    public EdiSegmentCommandHandler(IEdiSegmentRepository ediSegmentRepository, IEdiModelRepository ediModelRepository)
    {
        _ediSegmentRepository = ediSegmentRepository;
        _ediModelRepository = ediModelRepository;
    }

    public async Task<ValidationResult> Handle(RegisterEdiSegmentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //create new instance of edi segment
        var ediSegment = new EdiSegment(Guid.NewGuid(), request.Model_Id, request.Title, request.Description);
        //check that model exists
        var ediModel = await _ediModelRepository.GetByIdAsync(ediSegment.Model_Id);
        if (ediModel == null)
        {
            AddError("Model not found");
            return ValidationResult;
        }

        //pass event to domain
        ediSegment.AddDomainEvent(new EdiSegmentRegisteredEvent(ediSegment.Id, ediSegment.Model_Id, ediSegment.Title,
            ediSegment.Description));
        //pass to repo
        _ediSegmentRepository.Add(ediSegment);
        //commit unit of work
        return await Commit(_ediSegmentRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiSegmentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //check that segment exists
        var ediSegment = await _ediSegmentRepository.GetByIdAsync(request.Id);
        if (ediSegment == null)
        {
            AddError("Segment not found");
            return ValidationResult;
        }

        //pass event to domain
        ediSegment.AddDomainEvent(new EdiSegmentRemovedEvent(ediSegment.Id));
        //pass to repo
        _ediSegmentRepository.Delete(ediSegment);
        //commit unit of work
        return await Commit(_ediSegmentRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiSegmentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //check that segment exists
        var ediSegment = await _ediSegmentRepository.GetByIdAsync(request.Id);
        if (ediSegment == null)
        {
            AddError("Segment not found");
            return ValidationResult;
        }

        //check that model id is valid
        var ediModel = await _ediModelRepository.GetByIdAsync(request.Model_Id);
        if (ediModel == null)
        {
            AddError("Model not found");
            return ValidationResult;
        }

        ediSegment.Model_Id = request.Model_Id;
        ediSegment.Title = request.Title;
        ediSegment.Description = request.Description;
        //pass event to domain
        ediSegment.AddDomainEvent(new EdiSegmentUpdatedEvent(ediSegment.Id, ediSegment.Model_Id, ediSegment.Title,
            ediSegment.Description));
        //pass to repo
        _ediSegmentRepository.Update(ediSegment);
        //commit unit of work
        return await Commit(_ediSegmentRepository.UnitOfWork);
    }
}