using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Whmcs.Connector.Core;

namespace Play.Domain.Whmcs.Commands;

public class WhmcsAcceptOrderCommandHandler : CommandHandler, IRequestHandler<AcceptWhmcsOrderCommand, ValidationResult>
{
    private readonly WhmcsApi _whmcsApi;

    public WhmcsAcceptOrderCommandHandler(WhmcsApi whmcsApi)
    {
        _whmcsApi = whmcsApi;
    }

    public Task<ValidationResult> Handle(AcceptWhmcsOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return Task.FromResult(request.ValidationResult);

        //accept order
        try
        {
            var ao = request.AcceptOrder;
            var result = _whmcsApi.OrderCommands.AcceptOrder(ao.OrderId, ao.ServerId ?? 0, ao.Username, ao.Password,
                ao.Registrar, ao.SendRegistrar ?? false, ao.SendModule ?? false, ao.SendEmail ?? false);

            return Task.FromResult(ValidationResult);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return Task.FromResult(ValidationResult);
        }
    }
}