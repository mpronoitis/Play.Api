using Play.Domain.Whmcs.ResponseModels;

namespace Play.Domain.Whmcs.Models;

public class WhmcsAdminUser
{
    public int id { get; set; }
    public string uuid { get; set; } = null!;
    public int roleId { get; set; }
    public string username { get; set; } = null!;
    public string twoFactorAuthModule { get; set; } = null!;
    public string firstname { get; set; } = null!;
    public string lastname { get; set; } = null!;
    public string email { get; set; } = null!;
    public string signature { get; set; } = null!;
    public string notes { get; set; } = null!;
    public string template { get; set; } = null!;
    public string language { get; set; } = null!;
    public int isDisabled { get; set; }
    public int loginAttempts { get; set; }
    public List<string> supportDepartmentIds { get; set; } = null!;
    public List<string> receivesTicketNotifications { get; set; } = null!;
    public string homeWidgets { get; set; } = null!;
    public string hiddenWidgets { get; set; } = null!;
    public string widgetOrder { get; set; } = null!;
    public WhmcsGetAdminUsersModelUserPreferences userPreferences { get; set; } = null!;
    public string createdAt { get; set; } = null!;
    public string updatedAt { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string gravatarHash { get; set; } = null!;
}