using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonHeItemRepository
{
    /// <summary>
    ///     Function to get a heitem based on its Heid (primary key) GUID
    /// </summary>
    /// <param name="heid">The GUID of the heitem</param>
    /// <returns>The heitem</returns>
    Task<Heitems?> GetHeitemByHeid(Guid heid);

    /// <summary>
    ///     Find items based on its name (hename)
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <returns>A list of items</returns>
    Task<List<Heitems>> FindHeitemsByName(string name);

    /// <summary>
    ///     Find items based on its code (hecode)
    /// </summary>
    /// <param name="code">The code of the item</param>
    /// <returns>A list of items</returns>
    Task<List<Heitems>> FindHeitemsByCode(string code);

    /// <summary>
    ///     Get All items
    /// </summary>
    /// <returns>A list of items</returns>
    Task<List<Heitems>> GetAllHeitems();

    /// <summary>
    ///     Get count of items created in a given period
    /// </summary>
    /// <param name="from">The start date of the period</param>
    /// <param name="to">The end date of the period</param>
    /// <returns>The count of items</returns>
    Task<int> GetHeitemsCountByPeriod(DateTime from, DateTime to);
}