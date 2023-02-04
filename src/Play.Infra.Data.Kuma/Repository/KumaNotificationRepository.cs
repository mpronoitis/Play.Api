using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Play.Domain.Kuma.Interfaces;
using Play.Domain.Kuma.Models;
using Play.Infra.Data.Context;

namespace Play.Infra.Data.Kuma.Repository;

public class KumaNotificationRepository : IKumaNotificationRepository
{
    protected readonly PlayContext Db;
    protected readonly DbSet<KumaNotification> DbSet;

    public KumaNotificationRepository(PlayContext db)
    {
        Db = db;
        DbSet = Db.Set<KumaNotification>();
    }

    //we will use unitOfWork pattern to make sure that controllers make use of same context
    public IUnitOfWork UnitOfWork => Db;

    /// <summary>
    ///     Get all notifications with pagination
    /// </summary>
    public async Task<IEnumerable<KumaNotification>> GetAllWithPaging(int page = 1, int pageSize = 10)
    {
        return await DbSet.AsNoTracking().OrderByDescending(x => x.ReceivedAt).Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Get latest entry for a given url , order by receivedAt desc
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<KumaNotification> GetLatestByUrl(string url)
    {
        //check if any url contains the given url
        return await DbSet.AsNoTracking().Where(x => x.monitor.Url.Contains(url)).OrderByDescending(x => x.ReceivedAt)
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException("No notification found for given url");
    }

    /// <summary>
    ///     Return incidents for a given url and a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="url"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<IEnumerable<KumaNotification>> GetIncidentsByUrlAndTimeRange(string url, DateTime from,
        DateTime to)
    {
        return await DbSet.AsNoTracking()
            .Where(x => x.monitor.Url.Contains(url) && x.ReceivedAt >= from && x.ReceivedAt <= to)
            .OrderByDescending(x => x.ReceivedAt).ToListAsync();
    }

    /// <summary>
    ///     Return incidents for a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<IEnumerable<KumaNotification>> GetIncidentsByTimeRange(DateTime from, DateTime to)
    {
        return await DbSet.AsNoTracking().Where(x => x.ReceivedAt >= from && x.ReceivedAt <= to)
            .OrderByDescending(x => x.ReceivedAt).ToListAsync();
    }


    /// <summary>
    ///     Add a notification to the database
    /// </summary>
    public void Add(KumaNotification notification)
    {
        DbSet.Add(notification);
    }


    public void Dispose()
    {
        Db.Dispose();
    }
}