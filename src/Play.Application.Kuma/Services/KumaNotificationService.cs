using NetDevPack.Messaging;
using Play.Application.Kuma.Interfaces;
using Play.Application.Kuma.ViewModels;
using Play.Domain.Kuma.Interfaces;
using Play.Domain.Kuma.Models;

namespace Play.Application.Kuma.Services;

public class KumaNotificationService : CommandHandler, IKumaNotificationService
{
    private readonly IKumaNotificationRepository _kumaNotificationRepository;

    public KumaNotificationService(IKumaNotificationRepository kumaNotificationRepository)
    {
        _kumaNotificationRepository = kumaNotificationRepository;
    }

    /// <summary>
    ///     Receive a notification from a kuma web hook
    ///     We will parse the view model to a notification and save it to the database
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public async Task ReceiveNotificationAsync(KumaNotificationViewModel notification)
    {
        //map view model to domain model
        if (notification.heartbeat != null)
            if (notification.monitor != null)
            {
                var kumaNotification = new KumaNotification
                {
                    Id = Guid.NewGuid(),
                    msg = notification.msg,
                    heartbeat = new KumaHeartbeat
                    {
                        Id = Guid.NewGuid(),
                        MonitorID = notification.heartbeat.monitorID ?? 0,
                        Status = notification.heartbeat.status ?? 0,
                        Time = notification.heartbeat.time ?? string.Empty,
                        Msg = notification.heartbeat.msg ?? string.Empty,
                        Important = notification.heartbeat.important ?? false,
                        Duration = notification.heartbeat.duration ?? 0
                    },
                    monitor = new KumaMonitor
                    {
                        Id = Guid.NewGuid(),
                        Name = notification.monitor.name ?? string.Empty,
                        Url = notification.monitor.url ?? string.Empty,
                        Hostname = notification.monitor.hostname ?? string.Empty,
                        Port = notification.monitor.port ?? string.Empty,
                        Maxretries = notification.monitor.maxretries ?? 0,
                        Weight = notification.monitor.weight ?? 0,
                        Active = notification.monitor.active ?? 0,
                        Type = notification.monitor.type ?? string.Empty,
                        Interval = notification.monitor.interval ?? 0,
                        Keyword = notification.monitor.keyword ?? string.Empty
                    },
                    ReceivedAt = DateTime.Now
                };

                //save to database
                _kumaNotificationRepository.Add(kumaNotification);
            }

        await Commit(_kumaNotificationRepository.UnitOfWork);
    }

    /// <summary>
    ///     Get all with pagination
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IEnumerable<KumaNotification>> GetAllAsync(int page, int pageSize)
    {
        return await _kumaNotificationRepository.GetAllWithPaging(page, pageSize);
    }

    /// <summary>
    ///     Get latest entry for a given url , order by receivedAt desc
    /// </summary>
    /// <param name="url">The url to search for</param>
    /// <returns></returns>
    public async Task<KumaNotification> GetLatestByUrlAsync(string url)
    {
        return await _kumaNotificationRepository.GetLatestByUrl(url);
    }

    /// <summary>
    ///     Return incidents for a given url and a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="url"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<IEnumerable<KumaNotification>> GetIncidentsByUrlAsync(string url, DateTime from, DateTime to)
    {
        return await _kumaNotificationRepository.GetIncidentsByUrlAndTimeRange(url, from, to);
    }

    /// <summary>
    ///     Return incidents for a given time range , order by receivedAt desc
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<IEnumerable<KumaNotification>> GetIncidentsByTimeRangeAsync(DateTime from, DateTime to)
    {
        return await _kumaNotificationRepository.GetIncidentsByTimeRange(from, to);
    }
}