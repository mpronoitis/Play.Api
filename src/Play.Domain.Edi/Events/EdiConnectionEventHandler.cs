using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Edi.Events;

public class EdiConnectionEventHandler :
    INotificationHandler<EdiConnectionRegisteredEvent>,
    INotificationHandler<EdiConnectionRemovedEvent>,
    INotificationHandler<EdiConnectionUpdatedEvent>
{
    public Task Handle(EdiConnectionRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiConnectionRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiConnectionUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiConnectionRegisteredEvent : Event
{
    public Guid Id;

    //event constructor
    public EdiConnectionRegisteredEvent(Guid id, Guid customer_Id, Guid model_Id, Guid org_Id, Guid profile_Id,
        string ftp_Hostname, string ftp_Username, string ftp_Password, int ftp_port,string file_type)
    {
        Id = id;
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Org_Id = org_Id;
        Profile_Id = profile_Id;
        Ftp_Hostname = ftp_Hostname;
        Ftp_Username = ftp_Username;
        Ftp_Password = ftp_Password;
        Ftp_port = ftp_port;
        File_type = file_type;
    }

    //the customer related to this connection
    public Guid Customer_Id { get; set; }

    // the Edi model id related to this connection
    public Guid Model_Id { get; set; }

    //the Edi organization id related to this connection
    public Guid Org_Id { get; set; }

    //the Edi profile id related to this connection
    public Guid Profile_Id { get; set; }

    //the hostname of the ftp server
    public string Ftp_Hostname { get; set; }

    //the ftp username
    public string Ftp_Username { get; set; }

    //the ftp password
    public string Ftp_Password { get; set; }
    
    //the ftp port
    public int Ftp_port { get; set; }
    
    //the file type
    public string File_type { get; set; }
}

public class EdiConnectionRemovedEvent : Event
{
    //event constructor
    public EdiConnectionRemovedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class EdiConnectionUpdatedEvent : Event
{
    //event constructor
    public EdiConnectionUpdatedEvent(Guid id, Guid customer_Id, Guid model_Id, Guid org_Id, Guid profile_Id,
        string ftp_Hostname, string ftp_Username, string ftp_Password,int ftp_port,string file_type)
    {
        Id = id;
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Org_Id = org_Id;
        Profile_Id = profile_Id;
        Ftp_Hostname = ftp_Hostname;
        Ftp_Username = ftp_Username;
        Ftp_Password = ftp_Password;
        Ftp_Port = ftp_port;
        File_Type = file_type;
    }

    public Guid Id { get; set; }

    //the customer related to this connection
    public Guid Customer_Id { get; set; }

    // the Edi model id related to this connection
    public Guid Model_Id { get; set; }

    //the Edi organization id related to this connection
    public Guid Org_Id { get; set; }

    //the Edi profile id related to this connection
    public Guid Profile_Id { get; set; }

    //the hostname of the ftp server
    public string Ftp_Hostname { get; set; }

    //the ftp username
    public string Ftp_Username { get; set; }

    //the ftp password
    public string Ftp_Password { get; set; }
    
    //the ftp port
    public int Ftp_Port { get; set; }
    
    //the file type
    public string File_Type { get; set; }
}