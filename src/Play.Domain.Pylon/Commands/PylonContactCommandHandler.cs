using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Pylon.Interfaces;

namespace Play.Domain.Pylon.Commands;

public class PylonContactCommandHandler : CommandHandler,
    IRequestHandler<RegisterPylonContactCommand, ValidationResult>,
    IRequestHandler<RegisterListPylonContactCommand, ValidationResult>,
    IRequestHandler<RemoveAllPylonContactCommand, ValidationResult>
{
    private readonly IPylonTempContactRepository _pylonTempContactRepository;

    public PylonContactCommandHandler(IPylonTempContactRepository pylonTempContactRepository)
    {
        _pylonTempContactRepository = pylonTempContactRepository;
    }

    public async Task<ValidationResult> Handle(RegisterListPylonContactCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _pylonTempContactRepository.AddRange(request.PylonContacts);
            return await Commit(_pylonTempContactRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(RegisterPylonContactCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _pylonTempContactRepository.Add(request.PylonContact);
            return await Commit(_pylonTempContactRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(RemoveAllPylonContactCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            _pylonTempContactRepository.RemoveAll();
            return await Commit(_pylonTempContactRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }
}