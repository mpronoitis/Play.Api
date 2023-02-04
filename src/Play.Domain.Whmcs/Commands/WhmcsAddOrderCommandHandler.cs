using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using Play.Whmcs.Connector.Core;

namespace Play.Domain.Whmcs.Commands;

public class WhmcsAddOrderCommandHandler : CommandHandler, IRequestHandler<AddWhmcsOrderCommand, ValidationResult>
{
    private readonly WhmcsApi _whmcsApi;

    public WhmcsAddOrderCommandHandler(WhmcsApi whmcsApi)
    {
        _whmcsApi = whmcsApi;
    }

    public async Task<ValidationResult> Handle(AddWhmcsOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        var order = request.Order;

        try
        {
            var result = await _whmcsApi.OrderCommands.AddOrder(order.ClientId, order.PaymentMethod, order.ProductIds,
                order.DomainNames, order.BillingCycles, order.DomainRegTypes, order.DomainRegPeriods,
                order.DomainIdnLangs, order.DomainEppCodes, order.FirstNameserver ?? string.Empty,
                order.SecondNameserver ?? string.Empty,
                order.ThirdNameserver ?? string.Empty, order.FourthNameserver ?? string.Empty,
                order.FifthNameserver ?? string.Empty, order.CustomFields,
                order.ConfigOptions, order.OverridePrice, order.PromoCode ?? string.Empty, order.PromoOverride,
                order.AffiliateId,
                order.NoInvoice, order.NoInvoiceEmail, order.NoEmail, order.Addons ?? string.Empty,
                order.ServerHostname ?? string.Empty,
                order.ServerNameserver1 ?? string.Empty, order.ServerNameserver2 ?? string.Empty,
                order.ServerRootPassword ?? string.Empty, order.DomainContactId,
                order.DomainDnsManagement, order.TldSpecificFields, order.DomainEmailForwarding,
                order.DomainIdProtection, order.DomainOverridePrice, order.DomainOverrideRenewalPrice,
                order.DomainRenewals, order.IpAddress ?? string.Empty, order.AddonId, order.AddonIds,
                order.AddonServiceIds);

            return ValidationResult;
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return ValidationResult;
        }
    }
}