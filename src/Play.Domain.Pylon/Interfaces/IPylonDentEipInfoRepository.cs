namespace Play.Domain.Pylon.Interfaces;

public interface IPylonDentEipInfoRepository
{
    /// <summary>
    ///     Function to get the Heqrcode for a given heid (primary key) GUID
    /// </summary>
    /// <param name="dentid">The heid GUID</param>
    /// <returns>The heqrcode</returns>
    string? GetHeqrcodeByDentid(Guid dentid);
}