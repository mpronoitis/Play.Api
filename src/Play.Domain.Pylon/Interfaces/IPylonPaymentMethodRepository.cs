using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonPaymentMethodRepository
{
    /// <summary>
    ///     Get payment method by heid (primary key) GUID
    /// </summary>
    /// <param name="heid">GUID</param>
    /// <returns>PaymentMethod</returns>
    Task<Hepaymentmethods?> GetPaymentMethodByHeid(Guid heid);
}