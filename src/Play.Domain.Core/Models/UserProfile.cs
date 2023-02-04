using System;
using NetDevPack.Domain;

namespace Play.Domain.Core.Models;

public class UserProfile : Entity, IAggregateRoot
{
    public UserProfile(Guid id, Guid userId, string firstName, string lastName, DateTime dateOfBirth,
        string companyName, string languagePreference, string themePreference, string Tin)
    {
        Id = id;
        User_Id = userId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CompanyName = companyName;
        LanguagePreference = languagePreference;
        ThemePreference = themePreference;
        TIN = Tin;
    }

    // Empty constructor for EF
    protected UserProfile()
    {
    }

    /**
     * The user profile contains details regarding the user
     */
    public Guid User_Id { get; set; }

    /// <summary>
    ///     The first name of the user
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     The last name of the user
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     Date of birth of the user
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    ///     Company name of the user
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    ///     The users language preference (en or el)
    /// </summary>
    public string LanguagePreference { get; set; }

    /// <summary>
    ///     The users prefered theme (light or dark)
    /// </summary>
    public string ThemePreference { get; set; }

    /// <summary>
    ///     The users TIN
    /// </summary>
    public string TIN { get; set; }
}