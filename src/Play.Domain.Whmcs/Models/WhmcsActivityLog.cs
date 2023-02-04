namespace Play.Domain.Whmcs.Models;

public class WhmcsActivityLog
{
    public string id { get; set; } = null!;
    public string clientId { get; set; } = null!;
    public string adminId { get; set; } = null!;
    public string date { get; set; } = null!;
    public string description { get; set; } = null!;
    public string username { get; set; } = null!;
    public string ipaddress { get; set; } = null!;
    public string userid { get; set; } = null!;
}