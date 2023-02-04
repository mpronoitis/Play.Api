using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Interfaces;

public interface IPylonInvoiceBuilderService
{
    /// <summary>
    ///     Builds all available <see cref="PylonInvoice" /> for the given <paramref name="userId" />.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns> A collection of <see cref="PylonInvoice" />.</returns>
    Task<IEnumerable<PylonInvoice>> BuildAll(Guid userId);
}