using NetDevPack.Domain;

namespace Play.Domain.Contracting.Models;

public class Contract : Entity, IAggregateRoot
{
    //empty constructor for EF
    public Contract()
    {
    }

    public Contract(Guid id, string code, string clientName, string clientTin, string itemName, string status,
        DateTime startDate, DateTime endDate, DateTime createdAt, Guid clientId, Guid itemId)
    {
        Id = id;
        Code = code;
        ClientName = clientName;
        ClientTin = clientTin;
        ItemName = itemName;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
        CreatedAt = createdAt;
        ClientId = clientId;
        ItemId = itemId;
    }

    /// <summary>
    ///     The code of the contract
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    ///     The name of the client
    /// </summary>
    public string ClientName { get; set; } = null!;

    /// <summary>
    ///     The clients TIN
    /// </summary>
    public string ClientTin { get; set; } = null!;

    /// <summary>
    ///     The item's name
    /// </summary>
    public string ItemName { get; set; } = null!;

    /// <summary>
    ///     The status of the contract
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    ///     The contracts start date
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     The contracts end date
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     The createdAt date
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     The heid of the client (hecontacts id)
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    ///     The heid of the item (heitems id)
    /// </summary>
    public Guid ItemId { get; set; }

    //set contract code to "SYMB-{ddMM}-{clientTin}"
    public void SetCode()
    {
        var date = DateTime.Now;
        var day = date.Day.ToString().PadLeft(2, '0');
        var month = date.Month.ToString().PadLeft(2, '0');
        Code = $"SYMB-{day}{month}-{ClientTin}";
    }
}