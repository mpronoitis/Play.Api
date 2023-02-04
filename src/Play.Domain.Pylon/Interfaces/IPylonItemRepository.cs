using NetDevPack.Data;
using Play.Domain.Pylon.Models;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonItemRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<PylonItem?> GetById(Guid id);

    /// <summary>
    ///     Get Item by code
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<PylonItem?> GetByCode(string code);

    /// <summary>
    ///     Get All Items with paging
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<IEnumerable<PylonItem>> GetAll(int page, int pageSize);

    /// <summary>
    ///     Add Item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    void Add(PylonItem item);

    /// <summary>
    ///     Add list of Items
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    void AddRange(IEnumerable<PylonItem> items);

    /// <summary>
    ///     Remove all Items
    /// </summary>
    /// <returns></returns>
    void RemoveAll();

    /// <summary>
    ///     Get Items by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<IEnumerable<PylonItem>> GetByName(string name);

    /// <summary>
    ///     Get total count of Items
    /// </summary>
    /// <returns></returns>
    Task<int> GetCount();

    /// <summary>
    ///     Remove Item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    void Remove(PylonItem item);

    void Dispose();

    /// <summary>
    ///     Remove range
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    void RemoveRange(IEnumerable<PylonItem> items);
}