using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Pylon.Interfaces;

namespace Play.Domain.Pylon.Commands;

public class PylonInvoiceCommandHandler : CommandHandler,
    IRequestHandler<RegisterPylonInvoiceCommand, ValidationResult>,
    IRequestHandler<RemovePylonInvoiceCommand, ValidationResult>
{
    private readonly IPylonInvoiceRepository _pylonInvoiceRepository;

    public PylonInvoiceCommandHandler(IPylonInvoiceRepository pylonInvoiceRepository)
    {
        _pylonInvoiceRepository = pylonInvoiceRepository;
    }

    public async Task<ValidationResult> Handle(RegisterPylonInvoiceCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        _pylonInvoiceRepository.Add(request.PylonInvoice);

        return await Commit(_pylonInvoiceRepository.UnitOfWork);
    }


    public async Task<ValidationResult> Handle(RemovePylonInvoiceCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        //get the pylonInvoice
        var pylonInvoice = await _pylonInvoiceRepository.GetById(request.Id);
        if (pylonInvoice is null)
        {
            AddError("The pylonInvoice does not exist");
            return ValidationResult;
        }

        _pylonInvoiceRepository.Remove(pylonInvoice);

        return await Commit(_pylonInvoiceRepository.UnitOfWork);
    }
}