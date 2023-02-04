using NetDevPack.Mediator;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Commands;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;

namespace Play.Application.Pylon.Services;

public class PylonContactMigrationService : IPylonContactMigrationService
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IPylonHeContactRepository _pylonHeContactRepository;

    public PylonContactMigrationService(IMediatorHandler mediatorHandler,
        IPylonHeContactRepository pylonHeContactRepository)
    {
        _mediatorHandler = mediatorHandler;
        _pylonHeContactRepository = pylonHeContactRepository;
    }

    /// <summary>
    ///     Function that migrates the contacts from the pylon db to the PlayPylon db
    /// </summary>
    /// <returns></returns>
    public async Task MigrateHeContacts()
    {
        //get all the contacts from the pylon db
        var hecontacts = await _pylonHeContactRepository.GetAllContacts();
        var contacts = hecontacts.Select(hecontact => new PylonContact
            {
                Id = Guid.NewGuid(),
                Heid = hecontact.Heid,
                Code = CutString(hecontact.Hecode, 50),
                Name = CutString(hecontact.Hename, 100),
                FirstName = CutString(hecontact.Hefirstname ?? "", 100),
                LastName = CutString(hecontact.Helastname ?? "", 100),
                Tin = CutString(hecontact.Hetin ?? "", 50),
                Emails = CutString(
                    (!string.IsNullOrEmpty(hecontact.Heemail1) ? hecontact.Heemail1 : "") +
                    (!string.IsNullOrEmpty(hecontact.Heemail2) ? "," + hecontact.Heemail2 : "") +
                    (!string.IsNullOrEmpty(hecontact.Heemail3) ? "," + hecontact.Heemail3 : ""), 500),
                Phones = CutString(
                    (!string.IsNullOrEmpty(hecontact.Hephone1) ? hecontact.Hephone1 : "") +
                    (!string.IsNullOrEmpty(hecontact.Hephone2) ? "," + hecontact.Hephone2 : "") +
                    (!string.IsNullOrEmpty(hecontact.Hephone3) ? "," + hecontact.Hephone3 : "") +
                    (!string.IsNullOrEmpty(hecontact.Hephone4) ? "," + hecontact.Hephone4 : "") +
                    (!string.IsNullOrEmpty(hecontact.Hephone5) ? "," + hecontact.Hephone5 : ""), 500),
                Address = CutString((!string.IsNullOrEmpty(hecontact.Hecity) ? hecontact.Hecity : "") +
                                    (!string.IsNullOrEmpty(hecontact.Hestreet) ? "," + hecontact.Hestreet : "") +
                                    (!string.IsNullOrEmpty(hecontact.Hestreetnumber)
                                        ? "," + hecontact.Hestreetnumber
                                        : "") +
                                    (!string.IsNullOrEmpty(hecontact.Hepostalcode) ? "," + hecontact.Hepostalcode : ""),
                    500),
                CreatedDate = hecontact.Hecreationdate
            })
            .ToList();

        //dispatch command to empty the table
        await _mediatorHandler.SendCommand(new RemoveAllPylonContactCommand());
        //dispatch command to insert the contacts
        await _mediatorHandler.SendCommand(new RegisterListPylonContactCommand(contacts));
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