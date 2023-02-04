namespace Play.BackgroundJobs.Pylon.Interfaces;

public interface IPylonInvoiceBuilderWorker
{
    Task DoWork();
}