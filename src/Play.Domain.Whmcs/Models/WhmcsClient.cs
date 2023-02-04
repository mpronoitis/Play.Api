namespace Play.Domain.Whmcs.Models;

public class WhmcsClient
{
    public string id { get; set; } = null!;
    public string firstname { get; set; } = null!;
    public string lastname { get; set; } = null!;
    public string companyname { get; set; } = null!;
    public string email { get; set; } = null!;
    public string datecreated { get; set; } = null!;
    public string groupid { get; set; } = null!;
    public string status { get; set; } = null!;
}