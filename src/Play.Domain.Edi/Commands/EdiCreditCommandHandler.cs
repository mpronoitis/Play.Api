using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Core.Interfaces;
using Play.Domain.Edi.Events;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Commands;

public class EdiCreditCommandHandler : CommandHandler, IRequestHandler<RegisterEdiCreditCommand, ValidationResult>,
    IRequestHandler<UpdateEdiCreditCommand, ValidationResult>, IRequestHandler<RemoveEdiCreditCommand, ValidationResult>
{
    private readonly IEdiCreditRepository _ediCreditRepository;
    private readonly IUserRepository _userRepository;

    public EdiCreditCommandHandler(IEdiCreditRepository ediCreditRepository, IUserRepository userRepository)
    {
        _ediCreditRepository = ediCreditRepository;
        _userRepository = userRepository;
    }

    public async Task<ValidationResult> Handle(RegisterEdiCreditCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //check if user exists
        var user = await _userRepository.GetByIdAsync(request.Credit.CustomerId);
        if (user == null)
        {
            AddError("User not found");
            return ValidationResult;
        }

        //check if credit already exists for this user
        var credit = await _ediCreditRepository.GetByCustomerIdAsync(request.Credit.CustomerId);
        if (credit != null)
        {
            AddError("Credit already exists for this user");
            return ValidationResult;
        }

        var entity = new EdiCredit(request.Credit.Id, request.Credit.CustomerId, request.Credit.Amount,
            request.Credit.CreatedAt);
        entity.AddDomainEvent(new EdiCreditRegisteredEvent(entity.Id, entity.CustomerId, entity.Amount, entity.CreatedAt,entity.UpdatedAt));

        //add credit
        _ediCreditRepository.Add(entity);

        return await Commit(_ediCreditRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoveEdiCreditCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //find credit  by id
        var entity = await _ediCreditRepository.GetByCreditIdAsync(request.Id);

        if (entity == null)
        {
            AddError("Credit not found");
            return ValidationResult;
        }
        
        entity.AddDomainEvent(new EdiCreditRemovedEvent(entity.Id, entity.CustomerId, entity.Amount, entity.CreatedAt,entity.UpdatedAt));

        //remove credit
        _ediCreditRepository.Remove(entity);

        return await Commit(_ediCreditRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(UpdateEdiCreditCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //check if user exists
        var user = await _userRepository.GetByIdAsync(request.Credit.CustomerId);
        if (user == null)
        {
            AddError("User not found");
            return ValidationResult;
        }

        //check if credit already exists for this user
        var entity = await _ediCreditRepository.GetByCustomerIdAsync(request.Credit.CustomerId);
        if (entity == null)
        {
            AddError("Credit does not exist for this user");
            return ValidationResult;
        }

        entity.Id = request.Credit.Id;
        entity.CustomerId = request.Credit.CustomerId;
        entity.Amount = request.Credit.Amount;
        entity.UpdatedAt = request.Credit.UpdatedAt;
        
        entity.AddDomainEvent(new EdiCreditUpdatedEvent(entity.Id, entity.CustomerId, entity.Amount, entity.CreatedAt,entity.UpdatedAt));
        //update credit
        _ediCreditRepository.Update(entity);

        return await Commit(_ediCreditRepository.UnitOfWork);
    }
}