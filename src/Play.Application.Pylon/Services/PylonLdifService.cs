using System.Text;
using Microsoft.AspNetCore.Mvc;
using Play.Application.Pylon.Interfaces;
using Play.Domain.Pylon.Interfaces;

namespace Play.Application.Pylon.Services;

public class PylonLdifService : IPylonLdifService
{
    private readonly IPylonHeContactRepository _pylonHeContactRepository;

    public PylonLdifService(IPylonHeContactRepository pylonHeContactRepository)
    {
        _pylonHeContactRepository = pylonHeContactRepository;
    }


    public async Task<FileContentResult> ExportContactsLdif()
    {
        try
        {
            var contacts = _pylonHeContactRepository.GetAllContacts();
            var ldif = new StringBuilder();

            foreach (var contact in from contact in await contacts
                     where contact.Hephone1 != ""
                     where contact.Hename != ""
                     select contact)
            {
                ldif.AppendLine("dn: uid=" + contact.Heid + ",dc=playldap,dc=local");
                ldif.AppendLine("objectClass: inetOrgPerson");
                ldif.AppendLine("objectClass: organizationalPerson");
                ldif.AppendLine("objectClass: person");
                ldif.AppendLine("objectClass: top");

                ldif.AppendLine("cn: " + ConvertGreekToEnglish(contact.Hename));

                // check if contact.HEDISTINCTIVETITLE is empty
                // because otherwise you can'n import an empty attribute
                // inside the LDAP server
                if (contact.Hename != "")
                    ldif.AppendLine("sn: " + ConvertGreekToEnglish(contact.Hename));
                else if (contact.Helastname != null)
                    ldif.AppendLine("sn: " + ConvertGreekToEnglish(contact.Helastname));

                if (contact.Hephone1 != null && contact.Hephone1.StartsWith("69"))
                    ldif.AppendLine("mobile: " + contact.Hephone1);
                else
                    ldif.AppendLine("telephoneNumber: " + contact.Hephone1);

                // ldif.AppendLine("telephoneNumber: " + RemoveNonNumbers(contact.HEPHONE1));

                if (contact.Hephone2 != "")
                {
                    // check if contact.Hephone2 starts with 69
                    if (contact.Hephone2 != null && contact.Hephone2.StartsWith("69"))
                        ldif.AppendLine("mobileTelephoneNumber: " + RemoveNonNumbers(contact.Hephone2));
                    else if (contact.Hephone2 != null)
                        ldif.AppendLine("telephoneNumber: " + RemoveNonNumbers(contact.Hephone2));
                }

                if (contact.Hephone3 != "")
                    if (contact.Hephone3 != null)
                        ldif.AppendLine("departmentNumber: " + RemoveNonNumbers(contact.Hephone3));
                if (contact.Heemail1 != "") ldif.AppendLine("mail: " + contact.Heemail1);
                ldif.AppendLine("");
            }

            //create file 
            var file = new FileContentResult(Encoding.UTF8.GetBytes(ldif.ToString()), "text/plain");
            file.FileDownloadName = "contacts.ldif";
            return file;
        }
        catch (Exception ex)
        {
            return new FileContentResult(Encoding.UTF8.GetBytes(ex.Message), "text/plain");
        }
    }

    /// <summary>
    ///     Helper function to convert a string with greek letters to a string with english letters
    ///     We will create a dictionary with greek letters and their english equivalents
    /// </summary>
    /// <param name="input">The string to be converted</param>
    /// <returns>The converted string</returns>
    private string ConvertGreekToEnglish(string input)
    {
        var greekToEnglish = new Dictionary<string, string>();
        greekToEnglish.Add("Α", "A");
        greekToEnglish.Add("Β", "B");
        greekToEnglish.Add("Γ", "G");
        greekToEnglish.Add("Δ", "D");
        greekToEnglish.Add("Ε", "E");
        greekToEnglish.Add("Ζ", "Z");
        greekToEnglish.Add("Η", "H");
        greekToEnglish.Add("Θ", "Th");
        greekToEnglish.Add("Ι", "I");
        greekToEnglish.Add("Κ", "K");
        greekToEnglish.Add("Λ", "L");
        greekToEnglish.Add("Μ", "M");
        greekToEnglish.Add("Ν", "N");
        greekToEnglish.Add("Ξ", "X");
        greekToEnglish.Add("Ο", "O");
        greekToEnglish.Add("Π", "P");
        greekToEnglish.Add("Ρ", "R");
        greekToEnglish.Add("Σ", "S");
        greekToEnglish.Add("Τ", "T");
        greekToEnglish.Add("Υ", "Y");
        greekToEnglish.Add("Φ", "F");
        greekToEnglish.Add("Χ", "X");
        greekToEnglish.Add("Ψ", "Ps");
        greekToEnglish.Add("Ω", "O");
        greekToEnglish.Add("Ά", "A");
        greekToEnglish.Add("Έ", "E");
        greekToEnglish.Add("Ή", "H");
        greekToEnglish.Add("Ί", "I");
        greekToEnglish.Add("Ό", "O");
        greekToEnglish.Add("Ύ", "Y");
        greekToEnglish.Add("Ώ", "O");

        //lowercase
        greekToEnglish.Add("α", "a");
        greekToEnglish.Add("β", "b");
        greekToEnglish.Add("γ", "g");
        greekToEnglish.Add("δ", "d");
        greekToEnglish.Add("ε", "e");
        greekToEnglish.Add("ζ", "z");
        greekToEnglish.Add("η", "h");
        greekToEnglish.Add("θ", "th");
        greekToEnglish.Add("ι", "i");
        greekToEnglish.Add("κ", "k");
        greekToEnglish.Add("λ", "l");
        greekToEnglish.Add("μ", "m");
        greekToEnglish.Add("ν", "n");
        greekToEnglish.Add("ξ", "x");
        greekToEnglish.Add("ο", "o");
        greekToEnglish.Add("π", "p");
        greekToEnglish.Add("ρ", "r");
        greekToEnglish.Add("σ", "s");
        greekToEnglish.Add("τ", "t");
        greekToEnglish.Add("υ", "y");
        greekToEnglish.Add("φ", "f");
        greekToEnglish.Add("χ", "x");
        greekToEnglish.Add("ψ", "ps");
        greekToEnglish.Add("ω", "o");

        //special
        greekToEnglish.Add("ς", "s");
        greekToEnglish.Add("Ϊ", "I");
        greekToEnglish.Add("Ϋ", "Y");
        greekToEnglish.Add("ά", "a");
        greekToEnglish.Add("έ", "e");
        greekToEnglish.Add("ή", "h");
        greekToEnglish.Add("ί", "i");
        greekToEnglish.Add("ό", "o");
        greekToEnglish.Add("ύ", "y");
        greekToEnglish.Add("ώ", "o");

        //convert
        var output = "";
        foreach (var c in input)
            if (greekToEnglish.ContainsKey(c.ToString()))
                output += greekToEnglish[c.ToString()];
            else
                output += c;
        //return
        return output;
    }

    /// <summary>
    ///     Helper function to remove all non numbers from a telephone number string
    ///     It will also remove all spaces
    /// </summary>
    /// <param name="input">The string to be converted</param>
    /// <returns>The converted string</returns>
    private static string RemoveNonNumbers(string input)
    {
        return input.Where(c => c != ' ').Where(char.IsNumber).Aggregate("", (current, c) => current + c);
    }
}