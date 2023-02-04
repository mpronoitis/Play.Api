using NetDevPack.Data;
using Play.Domain.Pylon.Models;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonTempContactRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<PylonContact?> GetById(Guid id);

    /// <summary>
    ///     Get all PylonContacts with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    Task<IEnumerable<PylonContact>> GetAll(int page, int pageSize);

    /// <summary>
    ///     Search for PylonContacts
    /// </summary>
    /// <param name="query">Query to search</param>
    /// <param name="name">If we want to search by name</param>
    /// <param name="phone">If we want to search by phone</param>
    /// <param name="email">If we want to search by email</param>
    /// <param name="address">If we want to search by address</param>
    /// <returns></returns>
    Task<IEnumerable<PylonContact>> Search(string query, bool name, bool phone, bool email, bool address);

    /// <summary>
    ///     Add a new PylonContact
    /// </summary>
    /// <param name="pylonContact">PylonContact object</param>
    Task Add(PylonContact pylonContact);

    /// <summary>
    ///     Add a range of PylonContacts
    /// </summary>
    /// <param name="pylonContacts">PylonContact objects</param>
    /// <returns></returns>
    Task AddRange(IEnumerable<PylonContact> pylonContacts);

    /// <summary>
    ///     Update a PylonContact
    /// </summary>
    /// <param name="pylonContact">PylonContact object</param>
    void Update(PylonContact pylonContact);

    /// <summary>
    ///     Remove a PylonContact
    /// </summary>
    /// <param name="pylonContact">PylonContact object</param>
    void Remove(PylonContact pylonContact);

    void RemoveRange(IEnumerable<PylonContact> pylonContacts);

    /// <summary>
    ///     Remove all PylonContacts
    /// </summary>
    void RemoveAll();
}