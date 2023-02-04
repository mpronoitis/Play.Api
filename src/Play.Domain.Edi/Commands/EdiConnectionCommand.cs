using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;

namespace Play.Domain.Edi.Commands;

public class EdiConnectionCommand : Command
{
    public Guid Id { get; protected set; }

    //the customer related to this connection
    public Guid Customer_Id { get; protected set; }

    // the Edi model id related to this connection
    public Guid Model_Id { get; protected set; }

    //the Edi organization id related to this connection
    public Guid Org_Id { get; protected set; }

    //the Edi profile id related to this connection
    public Guid Profile_Id { get; protected set; }

    //the hostname of the ftp server
    public string Ftp_Hostname { get; protected set; }

    //the ftp username
    public string Ftp_Username { get; protected set; }

    //the ftp password
    public string Ftp_Password { get; protected set; }
    
    //the ftp port
    public int Ftp_Port { get; protected set; }
    
    //the file type
    public string File_Type { get; protected set; }
}

public class RegisterEdiConnectionCommand : EdiConnectionCommand
{
    public RegisterEdiConnectionCommand(Guid customer_Id, Guid model_Id, Guid org_Id, Guid profile_Id,
        string ftp_Hostname, string ftp_Username, string ftp_Password, int ftp_Port, string file_Type)
    {
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Org_Id = org_Id;
        Profile_Id = profile_Id;
        Ftp_Hostname = ftp_Hostname;
        Ftp_Username = ftp_Username;
        Ftp_Password = ftp_Password;
        Ftp_Port = ftp_Port;
        File_Type = file_Type;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEdiConnectionCommandValidation()
            .Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiConnectionCommand : EdiConnectionCommand
{
    public UpdateEdiConnectionCommand(Guid id, Guid customer_Id, Guid model_Id, Guid org_Id, Guid profile_Id,
        string ftp_Hostname, string ftp_Username, string ftp_Password, int ftp_Port, string file_Type)
    {
        Id = id;
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Org_Id = org_Id;
        Profile_Id = profile_Id;
        Ftp_Hostname = ftp_Hostname;
        Ftp_Username = ftp_Username;
        Ftp_Password = ftp_Password;
        Ftp_Port = ftp_Port;
        File_Type = file_Type;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiConnectionCommandValidation()
            .Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiConnectionCommand : EdiConnectionCommand
{
    public RemoveEdiConnectionCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiConnectionCommandValidation()
            .Validate(this);
        return ValidationResult.IsValid;
    }
}