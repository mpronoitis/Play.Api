using NetDevPack.Messaging;
using Play.Domain.Pylon.Models;

namespace Play.Domain.Pylon.Commands;

public class PylonItemCommand : Command
{
    public PylonItem Item { get; protected set; }
    public List<PylonItem> Items { get; protected set; }
}

public class RegisterPylonItemCommand : PylonItemCommand
{
    public RegisterPylonItemCommand(PylonItem item)
    {
        Item = item;
    }
}

public class RegisterListPylonItemCommand : PylonItemCommand
{
    public RegisterListPylonItemCommand(List<PylonItem> items)
    {
        Items = items;
    }
}

public class RemoveAllPylonItemsCommand : PylonItemCommand
{
}