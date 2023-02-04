namespace Play.Domain.Whmcs.Models;

public class WhmcsAddClient
{
    /// <summary>
    ///     First Name of the client Required
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    ///     Last Name of the client Required
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    ///     Company Name of the client - Optional
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    ///     Email Address of the client Required
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    ///     Address 1 of the client - Required
    /// </summary>
    public string Address1 { get; set; } = null!;

    /// <summary>
    ///     Address 2 of the client - Optional
    /// </summary>
    public string? Address2 { get; set; }

    /// <summary>
    ///     City of the client - Required
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    ///     State of the client - Required
    /// </summary>
    public string State { get; set; } = null!;

    /// <summary>
    ///     Postcode of the client - Required
    /// </summary>
    public string Postcode { get; set; } = null!;

    /// <summary>
    ///     Country Code of the client - Required
    /// </summary>
    public string Country { get; set; } = null!;

    /// <summary>
    ///     Phone Number of the client - Required
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    ///     Tax id of the client - Optional
    /// </summary>
    public string? TaxId { get; set; }

    /// <summary>
    ///     Password of the client - Required
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    ///     The ID of the security question in tbladminsecurityquestions. Required
    /// </summary>
    public int SecurityQuestionId { get; set; }

    /// <summary>
    ///     The answer to the security question. Optional
    /// </summary>
    public string? SecurityQuestionAnswer { get; set; }

    /// <summary>
    ///     Currency ID from tblcurrencies.Optional
    /// </summary>
    public int? CurrencyId { get; set; }

    /// <summary>
    ///     Client Group ID from tblclientgroups.Optional
    /// </summary>
    public int? ClientGroupId { get; set; }

    /// <summary>
    ///     Base64 encoded serialized array of custom field values.Optional
    /// </summary>
    public string? CustomFields { get; set; }

    /// <summary>
    ///     Default language setting. Also used for the language of the user when owner_user_id is not specified. Provide the
    ///     full name: ‘english’, ‘french’, etc….Optional
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    ///     The ID of the user that should own the client. Optional. When not provided, a new user will be created.Optional
    /// </summary>
    public int? OwnerUserId { get; set; }

    /// <summary>
    ///     The originating IP address for the request.Optional
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    ///     Admin only notes.Optional
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    ///     Whether the client should opt-in to receiving marketing emails.Optional
    /// </summary>
    public bool? MarketingEmailsOptIn { get; set; }

    /// <summary>
    ///     Whether to send the client a welcome email. A true value will not send the email.Optional
    /// </summary>
    public bool? NoEmail { get; set; }

    /// <summary>
    ///     Whether to enforce required fields. A true value will not enforce required fields. This does not apply to email and
    ///     password2 when owner_user_id is not specified.Optional
    /// </summary>
    public bool? SkipValidation { get; set; }
}