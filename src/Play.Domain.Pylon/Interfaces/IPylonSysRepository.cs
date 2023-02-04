namespace Play.Domain.Pylon.Interfaces;

public interface IPylonSysRepository
{
    Task<string> GetByKey(string pokey);
}