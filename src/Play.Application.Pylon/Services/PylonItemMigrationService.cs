using NetDevPack.Mediator;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Commands;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Services;

public class PylonItemMigrationService : IPylonItemMigrationService
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IPylonHeItemRepository _pylonHeItemRepository;

    public PylonItemMigrationService(IMediatorHandler mediatorHandler, IPylonHeItemRepository pylonHeItemRepository)
    {
        _mediatorHandler = mediatorHandler;
        _pylonHeItemRepository = pylonHeItemRepository;
    }

    /// <summary>
    ///     Function that migrates the heitems from pylon to Play.Pylon database
    /// </summary>
    /// <returns></returns>
    public async Task MigrateHeItems()
    {
        //get all items from pylon
        var heItems = await _pylonHeItemRepository.GetAllHeitems();

        //loop through all items and map them to the new model
        var items = heItems.Select(heItem => new PylonItem
            {
                Id = Guid.NewGuid(),
                Heid = heItem.Heid,
                Name = CutString(heItem.Hename, 100),
                Description = CutString(heItem.Hedetaileddescr ?? string.Empty, 100),
                AuxiliaryCode = CutString(heItem.Heauxiliarycode ?? string.Empty, 100),
                Code = CutString(heItem.Hecode ?? string.Empty, 100),
                Comments = CutString(heItem.Hecomments ?? string.Empty, 250),
                FactoryCode = CutString(heItem.Hefactorycode ?? string.Empty, 100),
                CreatedAt = heItem.Hecreationdate
            })
            .ToList();
        //dispatch command to empty the table
        await _mediatorHandler.SendCommand(new RemoveAllPylonItemsCommand());
        //dispatch command to insert all items
        await _mediatorHandler.SendCommand(new RegisterListPylonItemCommand(items));
    }


    /// <summary>
    ///     helper function to cut a string to a certain length
    ///     if the string is shorter than the length, the string is returned
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private string CutString(string str, int length)
    {
        return str.Length > length ? str[..length] : str;
    }
}