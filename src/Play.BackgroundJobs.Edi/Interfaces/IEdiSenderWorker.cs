namespace Play.BackgroundJobs.Edi.Interfaces;

public interface IEdiSenderWorker
{
    Task DoWork();
}