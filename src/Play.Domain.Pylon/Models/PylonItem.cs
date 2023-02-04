using NetDevPack.Domain;

namespace Play.Domain.Pylon.Models;

public class PylonItem : Entity, IAggregateRoot
{
    public PylonItem(Guid id, Guid heid, string code, string name, string description, string factoryCode,
        string auxiliaryCode, string comments, DateTime createdAt)
    {
        Id = id;
        Heid = heid;
        Code = code;
        Name = name;
        Description = description;
        FactoryCode = factoryCode;
        AuxiliaryCode = auxiliaryCode;
        Comments = comments;
        CreatedAt = createdAt;
    }

    public PylonItem()
    {
    }

    public Guid Heid { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string FactoryCode { get; set; } = null!;
    public string AuxiliaryCode { get; set; } = null!;
    public string Comments { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}