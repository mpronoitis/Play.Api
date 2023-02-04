using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Models;

/// <summary>
///     We will use a DTO because the contact object is huge and we don't need all of it.
/// </summary>
public class PylonContactDTO
{
    /// <summary>
    ///     This is the constructor that will be used to create the DTO from the contact object.
    /// </summary>
    /// <param name="contact">
    ///     <see cref="Hecontacts" />
    /// </param>
    public PylonContactDTO(Hecontacts contact)
    {
        Heid = contact.Heid;
        Code = contact.Hecode;
        Name = contact.Hename;
        //we want to add Heemail1 , Heemail2 and Heemail3 to the EmailAddresses array
        EmailAddresses = new string[3];
        EmailAddresses[0] = contact.Heemail1 ?? string.Empty;
        EmailAddresses[1] = contact.Heemail2 ?? string.Empty;
        EmailAddresses[2] = contact.Heemail3 ?? string.Empty;
        //we want to add Hephone1 , Hephone2 , Hephone3 , Hephone4 and Hephone5 to the PhoneNumbers array
        PhoneNumbers = new string[5];
        PhoneNumbers[0] = contact.Hephone1 ?? string.Empty;
        PhoneNumbers[1] = contact.Hephone2 ?? string.Empty;
        PhoneNumbers[2] = contact.Hephone3 ?? string.Empty;
        PhoneNumbers[3] = contact.Hephone4 ?? string.Empty;
        PhoneNumbers[4] = contact.Hephone5 ?? string.Empty;
        Address = contact.Heregion + " " + contact.Hecity + " " + contact.Hestreet + " " + contact.Hestreetnumber;
        Tin = contact.Hetin ?? "N/A";
    }

    public Guid Heid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string[] EmailAddresses { get; set; }
    public string[] PhoneNumbers { get; set; }
    public string Address { get; set; }
    public string Tin { get; set; }
}