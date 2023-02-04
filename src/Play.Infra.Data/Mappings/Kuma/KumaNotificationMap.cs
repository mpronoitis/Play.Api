namespace Play.Infra.Data.Mappings.Kuma;

public class KumaNotificationMap : IEntityTypeConfiguration<KumaNotification>
{
    public void Configure(EntityTypeBuilder<KumaNotification> builder)
    {
        //primary key
        builder.HasKey(x => x.Id);
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        //auto generated ReceivedAt timestamp
        builder.Property(c => c.ReceivedAt)
            .HasColumnName("ReceivedAt")
            .HasDefaultValueSql("getdate()");

        //one kuma notification has one kumaHeartbeat
        builder.OwnsOne(c => c.heartbeat, hb =>
        {
            hb.ToTable("KumaHeartbeat");
            hb.Property(c => c.Id)
                .HasColumnName("Id");
            hb.Property(c => c.MonitorID)
                .HasColumnName("MonitorID");
            hb.Property(c => c.Status)
                .HasColumnName("Status");
            hb.Property(c => c.Time)
                .HasColumnName("Time");
            hb.Property(c => c.Msg)
                .HasColumnName("Msg");
            hb.Property(c => c.Important)
                .HasColumnName("Important");
            hb.Property(c => c.Duration)
                .HasColumnName("Duration");
        });
        //one kuma notification has one kuma monitor
        builder.OwnsOne(c => c.monitor, m =>
        {
            m.ToTable("KumaMonitor");
            m.Property(c => c.Id)
                .HasColumnName("Id");
            m.Property(c => c.Name)
                .HasColumnName("Name");
            m.Property(c => c.Url)
                .HasColumnName("Url");
            m.Property(c => c.Hostname)
                .HasColumnName("Hostname");
            m.Property(c => c.Port)
                .HasColumnName("Port");
            m.Property(c => c.Maxretries)
                .HasColumnName("Maxretries");
            m.Property(c => c.Weight)
                .HasColumnName("Weight");
            m.Property(c => c.Active)
                .HasColumnName("Active");
            m.Property(c => c.Type)
                .HasColumnName("Type");
            m.Property(c => c.Interval)
                .HasColumnName("Interval");
            m.Property(c => c.Keyword)
                .HasColumnName("Keyword");
        });

        builder.Property(c => c.msg)
            .HasColumnName("msg");
    }
}