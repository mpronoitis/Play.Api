namespace Play.Domain._20i.Models;

public class TwentyPackageModelPackageLabel
{
    public string label { get; set; } = null!;
}

public class TwentyPackageModel
{
    public int id { get; set; }
    public bool beingManaged { get; set; }
    public DateTime created { get; set; }
    public bool enabled { get; set; }
    public string externalId { get; set; } = null!;
    public string location { get; set; } = null!;
    public string name { get; set; } = null!;
    public List<string> names { get; set; } = null!;
    public string packageTypeName { get; set; } = null!;
    public string packageTypePlatform { get; set; } = null!;
    public string platform { get; set; } = null!;
    public object productSpec { get; set; } = null!;
    public string serviceType { get; set; } = null!;
    public List<string> stackUsers { get; set; } = null!;
    public int typeRef { get; set; }
    public List<TwentyPackageModelPackageLabel> packageLabels { get; set; } = null!;
    public TwentyPackageModelWordpressManagerCache wordpressManagerCache { get; set; } = null!;
}

public class TwentyPackageModelWordpressManagerCache
{
    public object phpVersion { get; set; } = null!;
    public object staging { get; set; } = null!;
}