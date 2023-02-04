using Play.Application.Kuma.ViewModels;
using Play.Domain.Kuma.Models;

namespace Play.Application.Kuma.Interfaces;

public interface IKumaNotificationService
{
    /// <summary>
    ///     Receive a notification from a kuma web hook
    ///     We will parse the view model to a notification and save it to the database
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    Task ReceiveNotificationAsync(KumaNotificationViewModel notification);

    /// <summary>
    ///     Get all with pagination
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<IEnumerable<KumaNotification>> GetAllAsync(int page, int pageSize);

    /// <summary>
    ///     Get latest entry for a given url , order by receivedAt desc
    /// </summary>
    /// <param name="url">The url to search for</param>
    /// <returns></returns>
    Task<KumaNotification> GetLatestByUrlAsync(string url);

    /// <summary>
    ///     Return incidents for a given url and a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="url"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    Task<IEnumerable<KumaNotification>> GetIncidentsByUrlAsync(string url, DateTime from, DateTime to);

    /// <summary>
    ///     Return incidents for a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    Task<IEnumerable<KumaNotification>> GetIncidentsByTimeRangeAsync(DateTime from, DateTime to);
}