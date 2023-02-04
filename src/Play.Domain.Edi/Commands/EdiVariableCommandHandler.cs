using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Edi.Events;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Commands;

public class EdiVariableCommandHandler : CommandHandler,
    IRequestHandler<RegisterEdiVariableCommand, ValidationResult>,
    IRequestHandler<UpdateEdiVariableCommand, ValidationResult>,
    IRequestHandler<RemoveEdiVariableCommand, ValidationResult>
{
    private readonly IEdiVariableRepository _ediVariableRepository;

    public EdiVariableCommandHandler(IEdiVariableRepository ediVariableRepository)
    {
        _ediVariableRepository = ediVariableRepository;
    }

    public async Task<ValidationResult> Handle(RegisterEdiVariableCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        var ediVariable =
            new EdiVariable(Guid.NewGuid(), request.Description, request.Description, request.Placeholder);
        //check that no other variable has the same placeholder
        var existingVariable = await _ediVariableRepository.GetByPlaceholderAsync(request.Placeholder);
        //convert ienumerable to list to be able to use .Any()
        if (existingVariable.Any())
        {
            AddError($"Placeholder {request.Placeholder} is already in use");
            return ValidationResult;
        }

        //pass to domain
        ediVariable.AddDomainEvent(new EdiVariableRegisteredEvent(ediVariable.Id, ediVariable.Description,
            ediVariable.Description, ediVariable.Placeholder));
        //pass to repo
        _ediVariableRepository.Register(ediVariable);
        //commit unit of work
        return await Commit(_ediVariableRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiVariableCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        var ediVariable = await _ediVariableRepository.GetByIdAsync(request.Id);
        if (ediVariable == null)
        {
            AddError("EdiVariable not found");
            return ValidationResult;
        }

        //pass to domain
        ediVariable.AddDomainEvent(new EdiVariableRemovedEvent(request.Id));
        //pass to repo
        _ediVariableRepository.Remove(ediVariable);
        //commit unit of work
        return await Commit(_ediVariableRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiVariableCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        var ediVariable = await _ediVariableRepository.GetByIdAsync(request.Id);
        if (ediVariable == null)
        {
            AddError("EdiVariable not found");
            return ValidationResult;
        }

        //check that no other variable has the same placeholder
        var existingVariable = await _ediVariableRepository.GetByPlaceholderAsync(request.Placeholder);
        //the returned var is an enumerable, so we need to convert to list
        if (existingVariable.ToList().Any(x => x.Id != ediVariable.Id))
        {
            AddError($"Placeholder {request.Placeholder} is already in use");
            return ValidationResult;
        }

        ediVariable.Description = request.Description;
        ediVariable.Placeholder = request.Placeholder;
        ediVariable.Title = request.Title;
        //pass to domain
        ediVariable.AddDomainEvent(new EdiVariableUpdatedEvent(request.Id, request.Title, request.Description,
            request.Placeholder));
        //pass to repo
        _ediVariableRepository.Update(ediVariable);
        //commit unit of work
        return await Commit(_ediVariableRepository.UnitOfWork);
    }
}