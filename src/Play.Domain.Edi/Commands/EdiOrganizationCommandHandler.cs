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

public class EdiOrganizationCommandHandler : CommandHandler,
    IRequestHandler<RegisterEdiOrganizationCommand, ValidationResult>,
    IRequestHandler<UpdateEdiOrganizationCommand, ValidationResult>,
    IRequestHandler<RemoveEdiOrganizationCommand, ValidationResult>
{
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;

    //constructor, inject repository
    public EdiOrganizationCommandHandler(IEdiOrganizationRepository ediOrganizationRepository)
    {
        _ediOrganizationRepository = ediOrganizationRepository;
    }

    public async Task<ValidationResult> Handle(RegisterEdiOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        var ediOrganization = new EdiOrganization(Guid.NewGuid(), request.Name, request.Email);
        //check that no other org has the same email
        var existingOrg = await _ediOrganizationRepository.GetByEmailAsync(request.Email);
        if (existingOrg != null)
        {
            AddError("Email exists on another organization");
            return ValidationResult;
        }

        ediOrganization.AddDomainEvent(new EdiOrganizationRegisteredEvent(ediOrganization.Id, ediOrganization.Name,
            ediOrganization.Email));
        _ediOrganizationRepository.Add(ediOrganization);
        //commit unit of work and return result
        return await Commit(_ediOrganizationRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        var ediOrganization = await _ediOrganizationRepository.GetByIdAsync(request.Id);
        if (ediOrganization == null)
        {
            AddError("Organization not found");
            return ValidationResult;
        }

        ediOrganization.AddDomainEvent(new EdiOrganizationRemovedEvent(ediOrganization.Id));
        _ediOrganizationRepository.Remove(ediOrganization);
        return await Commit(_ediOrganizationRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        var ediOrganization = await _ediOrganizationRepository.GetByIdAsync(request.Id);
        if (ediOrganization == null)
        {
            AddError("Organization not found");
            return ValidationResult;
        }

        ediOrganization.Email = request.Email;
        ediOrganization.Name = request.Name;

        ediOrganization.AddDomainEvent(new EdiOrganizationUpdatedEvent(ediOrganization.Id, ediOrganization.Name,
            ediOrganization.Email));
        _ediOrganizationRepository.Update(ediOrganization);
        return await Commit(_ediOrganizationRepository.UnitOfWork);
    }
}