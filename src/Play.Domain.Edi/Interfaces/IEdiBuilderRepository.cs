using System;
using System.Threading.Tasks;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiBuilderRepository
{
    /// <summary>
    ///     Function to build all un built documents for a given customer id
    /// </summary>
    /// <param name="customerId">Customer id</param>
    /// <returns>True if all documents were built, false otherwise</returns>
    Task<bool> BuildUnparsed(Guid customerId);
}