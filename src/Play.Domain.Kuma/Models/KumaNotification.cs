using NetDevPack.Domain;

namespace Play.Domain.Kuma.Models;

public class KumaNotification : Entity, IAggregateRoot
{
    ///empty constructor
    public KumaNotification()
    {
    }

    ///constructor
    public KumaNotification(Guid id, KumaHeartbeat heartbeat, KumaMonitor monitor, string msg)
    {
        Id = id;
        this.heartbeat = heartbeat;
        this.monitor = monitor;
        this.msg = msg;
    }

    public KumaHeartbeat heartbeat { get; set; } = null!;
    public KumaMonitor monitor { get; set; } = null!;
    public string msg { get; set; } = null!;
    public DateTime ReceivedAt { get; set; }
}

public class KumaHeartbeat : Entity, IAggregateRoot
{
    ///empty constructor
    public KumaHeartbeat()
    {
    }

    ///constructor
    public KumaHeartbeat(Guid id, int monitorId, int status, string time, string msg, bool important, int duration)
    {
        Id = id;
        MonitorID = monitorId;
        Status = status;
        Time = time;
        Msg = msg;
        Important = important;
        Duration = duration;
    }

    public int MonitorID { get; set; }
    public int Status { get; set; }
    public string Time { get; set; } = null!;
    public string Msg { get; set; } = null!;
    public bool Important { get; set; }
    public int Duration { get; set; }
}

public class KumaMonitor : Entity, IAggregateRoot
{
    ///empty constructor
    public KumaMonitor()
    {
    }

    ///constructor
    public KumaMonitor(Guid id, string name, string url, string hostname, string port, int maxretries, int weight,
        int active, string type, int interval, string keyword)
    {
        Id = id;
        Name = name;
        Url = url;
        Hostname = hostname;
        Port = port;
        Maxretries = maxretries;
        Weight = weight;
        Active = active;
        Type = type;
        Interval = interval;
        Keyword = keyword;
    }

    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Hostname { get; set; } = null!;
    public string Port { get; set; } = null!;
    public int Maxretries { get; set; }
    public int Weight { get; set; }
    public int Active { get; set; }
    public string Type { get; set; } = null!;
    public int Interval { get; set; }
    public string Keyword { get; set; } = null!;
}