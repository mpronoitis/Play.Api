using NetDevPack.Messaging;
using Play.Domain.Pylon.Models;

namespace Play.Domain.Pylon.Commands;

public class PylonContactCommand : Command
{
    public PylonContact PylonContact { get; protected set; } = null!;
    public List<PylonContact> PylonContacts { get; protected set; } = null!;
}

public class RegisterPylonContactCommand : PylonContactCommand
{
    public RegisterPylonContactCommand(PylonContact pylonContact)
    {
        PylonContact = pylonContact;
    }
}

public class RegisterListPylonContactCommand : PylonContactCommand
{
    public RegisterListPylonContactCommand(List<PylonContact> pylonContacts)
    {
        PylonContacts = pylonContacts;
    }
}

public class RemoveAllPylonContactCommand : PylonContactCommand
{
}