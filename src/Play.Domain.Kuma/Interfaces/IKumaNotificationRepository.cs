using NetDevPack.Data;
using Play.Domain.Kuma.Models;

namespace Play.Domain.Kuma.Interfaces;

public interface IKumaNotificationRepository
{
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    ///     Add a notification to the database
    /// </summary>
    void Add(KumaNotification notification);

    /// <summary>
    ///     Get all notifications with pagination
    /// </summary>
    Task<IEnumerable<KumaNotification>> GetAllWithPaging(int page = 1, int pageSize = 10);

    /// <summary>
    ///     Get latest entry for a given url , order by receivedAt desc
    /// </summary>
    /// <param name="url">The url to search for</param>
    /// <returns></returns>
    Task<KumaNotification> GetLatestByUrl(string url);

    /// <summary>
    ///     Return incidents for a given url and a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="url"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    Task<IEnumerable<KumaNotification>> GetIncidentsByUrlAndTimeRange(string url, DateTime from,
        DateTime to);

    /// <summary>
    ///     Return incidents for a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    Task<IEnumerable<KumaNotification>> GetIncidentsByTimeRange(DateTime from, DateTime to);

    void Dispose();
}