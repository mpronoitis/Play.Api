using System;
using System.Threading.Tasks;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiSendRepository
{
    /// <summary>
    ///     Function to send all unsent EDI files for a given customer_Id
    /// </summary>
    /// <param name="customerId">The customer primary key (GUID)</param>
    /// <returns>True if all files were sent, false if any failed</returns>
    Task<bool> SendUnsentEdiFiles(Guid customerId);
}