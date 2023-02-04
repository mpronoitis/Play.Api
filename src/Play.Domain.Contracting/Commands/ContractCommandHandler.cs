using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Contracting.Interfaces;

namespace Play.Domain.Contracting.Commands;

public class ContractCommandHandler : CommandHandler, IRequestHandler<RegisterContractCommand, ValidationResult>,
    IRequestHandler<UpdateContractCommand, ValidationResult>, IRequestHandler<RemoveContractCommand, ValidationResult>
{
    private readonly IContractRepository _contractRepository;

    public ContractCommandHandler(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<ValidationResult> Handle(RegisterContractCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            var contract = request.Contract;

            //set contract code
            contract.SetCode();
            //set createdAt
            contract.CreatedAt = DateTime.Now;

            _contractRepository.Add(contract);

            return await Commit(_contractRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(RemoveContractCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            //find contract by Id
            var contract = await _contractRepository.GetById(request.Id);
            //if null return error
            if (contract == null)
            {
                AddError("Contract not found");
                return ValidationResult;
            }

            _contractRepository.Remove(contract);

            return await Commit(_contractRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            var contract = request.Contract;


            _contractRepository.Update(contract);

            return await Commit(_contractRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }
}