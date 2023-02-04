using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.ResponseModels;

public class WhmcsGetAdminUsersModel
{
    public int count { get; set; }
    public List<WhmcsAdminUser> admin_users { get; set; } = null!;
}

public class WhmcsGetAdminUsersModelTableLengths
{
    public int @default { get; set; }
    public string summaryServices { get; set; } = null!;
}

public class WhmcsGetAdminUsersModelUserPreferences
{
    public WhmcsGetAdminUsersModelTableLengths tableLengths { get; set; } = null!;
}