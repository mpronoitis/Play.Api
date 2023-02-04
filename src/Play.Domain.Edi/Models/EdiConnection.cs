using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

public class EdiConnection : Entity, IAggregateRoot
{
    //constructor
    public EdiConnection(Guid id, Guid customer_id, Guid model_id, Guid org_id, Guid profile_id, string ftp_hostname,
        string ftp_username, string ftp_password, int ftp_port,string file_type)
    {
        Id = id;
        Customer_Id = customer_id;
        Model_Id = model_id;
        Org_Id = org_id;
        Profile_Id = profile_id;
        Ftp_Hostname = ftp_hostname;
        Ftp_Username = ftp_username;
        Ftp_Password = ftp_password;
        Ftp_Port = ftp_port;
        File_Type = file_type;
    }

    //empty constructor for ef
    public EdiConnection()
    {
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
    public int Ftp_Port { get; set; }
    
    //the file type
    public string File_Type { get; set; }
}