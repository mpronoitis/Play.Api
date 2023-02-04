namespace Play.Application.Pylon.Interfaces;

public interface IPylonSysService
{
    Task<string> GetByKey(string pokey);
}