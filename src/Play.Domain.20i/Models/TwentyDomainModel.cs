namespace Play.Domain._20i.Models;

public class TwentyDomainModelPackageInfo
{
    public string platform { get; set; } = null!;
    public string label { get; set; } = null!;
}

public class TwentyDomainModelRenewalConstraint
{
    public int gracePeriod { get; set; }
    public int redemptionPeriod { get; set; }
}

public class TwentyDomainModel
{
    public string deadDate { get; set; } = null!; //string because it can also return 00-00-0000
    public string expiryDate { get; set; } = null!; //string because it can also return 00-00-0000
    public int id { get; set; }
    public string name { get; set; } = null!;
    public bool pendingTransfer { get; set; }
    public TwentyDomainModelRenewalConstraint renewalConstraint { get; set; } = null!;
    public TwentyDomainModelPackageInfo packageInfo { get; set; } = null!;
    public string preferredRenewalAction { get; set; } = null!;
    public int preferredRenewalMonths { get; set; }
}