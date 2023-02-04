using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Pylon.Interfaces;

namespace Play.Domain.Pylon.Commands;

public class PylonItemCommandHandler : CommandHandler, IRequestHandler<RegisterPylonItemCommand, ValidationResult>,
    IRequestHandler<RemoveAllPylonItemsCommand, ValidationResult>,
    IRequestHandler<RegisterListPylonItemCommand, ValidationResult>
{
    private readonly IPylonItemRepository _pylonItemRepository;

    public PylonItemCommandHandler(IPylonItemRepository pylonItemRepository)
    {
        _pylonItemRepository = pylonItemRepository;
    }

    public async Task<ValidationResult> Handle(RegisterListPylonItemCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            _pylonItemRepository.AddRange(request.Items);
            return await Commit(_pylonItemRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }


    public async Task<ValidationResult> Handle(RegisterPylonItemCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            _pylonItemRepository.Add(request.Item);
            return await Commit(_pylonItemRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(RemoveAllPylonItemsCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        try
        {
            _pylonItemRepository.RemoveAll();
            return await Commit(_pylonItemRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }
}