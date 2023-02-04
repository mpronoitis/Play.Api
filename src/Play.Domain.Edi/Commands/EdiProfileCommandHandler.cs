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

public class EdiProfileCommandHandler : CommandHandler,
    IRequestHandler<RegisterEdiProfileCommand, ValidationResult>,
    IRequestHandler<UpdateEdiProfileCommand, ValidationResult>,
    IRequestHandler<RemoveEdiProfileCommand, ValidationResult>
{
    private readonly IEdiModelRepository _ediModelRepository;

    private readonly IEdiProfileRepository _ediProfileRepositor;
    //private readonly ICustomerRepository _customerRepository;

    public EdiProfileCommandHandler(IEdiProfileRepository ediProfileRepositor, IEdiModelRepository ediModelRepository)
    {
        _ediProfileRepositor = ediProfileRepositor;
        _ediModelRepository = ediModelRepository;
    }

    public async Task<ValidationResult> Handle(RegisterEdiProfileCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //create new EdiProfile
        var ediProfile = new EdiProfile(Guid.NewGuid(), request.Customer_Id, request.Model_Id, request.Title,
            request.Payload, request.Enabled);
        //check that the customer and model exist

        var model = await _ediModelRepository.GetByIdAsync(request.Model_Id);
        if (model is null)
        {
            AddError("Model not found");
            return ValidationResult;
        }

        //pass event to domain
        ediProfile.AddDomainEvent(new EdiProfileRegisteredEvent(ediProfile.Id, ediProfile.Customer_Id,
            ediProfile.Model_Id, ediProfile.Title, ediProfile.Payload, ediProfile.Enabled));
        //add model to repo
        _ediProfileRepositor.Add(ediProfile);
        //commit unit of work
        return await Commit(_ediProfileRepositor.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiProfileCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //get model from repository
        var ediProfile = await _ediProfileRepositor.GetByIdAsync(request.Id);
        if (ediProfile is null)
        {
            AddError("EdiProfile not found");
            return ValidationResult;
        }

        //pass to domain
        ediProfile.AddDomainEvent(new EdiProfileRemovedEvent(ediProfile.Id));
        //pass to repo
        _ediProfileRepositor.Remove(ediProfile);
        //commit unit of work
        return await Commit(_ediProfileRepositor.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiProfileCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //get model from repository
        var ediProfile = await _ediProfileRepositor.GetByIdAsync(request.Id);
        if (ediProfile is null)
        {
            AddError("EdiProfile not found");
            return ValidationResult;
        }


        var model = await _ediModelRepository.GetByIdAsync(request.Model_Id);
        if (model is null)
        {
            AddError("Model not found");
            return ValidationResult;
        }

        ediProfile.Customer_Id = request.Customer_Id;
        ediProfile.Model_Id = request.Model_Id;
        ediProfile.Title = request.Title;
        ediProfile.Payload = request.Payload;
        ediProfile.Enabled = request.Enabled;

        //update model
        ediProfile.AddDomainEvent(new EdiProfileUpdatedEvent(ediProfile.Id, ediProfile.Customer_Id, ediProfile.Model_Id,
            ediProfile.Title, ediProfile.Payload, ediProfile.Enabled));
        //pass to repo
        _ediProfileRepositor.Update(ediProfile);
        //commit unit of work
        return await Commit(_ediProfileRepositor.UnitOfWork);
    }
}