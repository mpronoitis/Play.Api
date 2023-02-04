using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Whmcs.Connector.Core;

namespace Play.Domain.Whmcs.Commands;

public class WhmcsAddClientCommandHandler : CommandHandler, IRequestHandler<AddWhmcsClientCommand, ValidationResult>
{
    private readonly WhmcsApi _whmcsApi;

    public WhmcsAddClientCommandHandler(WhmcsApi whmcsApi)
    {
        _whmcsApi = whmcsApi;
    }

    public async Task<ValidationResult> Handle(AddWhmcsClientCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        //create new instance of client
        var client = request.Client;
        try
        {
            //add client to whmcs
            var result = await _whmcsApi.ClientCommands.AddClient(client.FirstName, client.LastName, client.Email,
                client.Address1, client.City, client.State, client.Postcode, client.Country, client.PhoneNumber,
                client.CompanyName ?? string.Empty, client.Address2 ?? string.Empty, client.TaxId ?? string.Empty,
                client.Password, client.SecurityQuestionId,
                client.SecurityQuestionAnswer ?? string.Empty, client.CurrencyId ?? 0, client.ClientGroupId ?? 0,
                client.CustomFields ?? string.Empty,
                client.Language ?? string.Empty, client.OwnerUserId ?? 0, client.IpAddress ?? string.Empty,
                client.Notes ?? string.Empty, client.MarketingEmailsOptIn ?? false,
                client.NoEmail ?? false, client.SkipValidation ?? false);

            //return success validation result
            return ValidationResult;
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return ValidationResult;
        }
    }
}