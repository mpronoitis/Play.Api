using AutoMapper;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Models;

namespace Play.Application.Edi.AutoMapper;

public class EdiViewModelToDomainMapping
{
    public static void RegisterMappings(Profile profile)
    {
        //Edi Organization
        profile.CreateMap<EdiOrganizationViewModel, RegisterEdiOrganizationCommand>()
            .ConstructUsing(c => new RegisterEdiOrganizationCommand(c.Name, c.Email));
        profile.CreateMap<EdiOrganizationViewModel, UpdateEdiOrganizationCommand>()
            .ConstructUsing(c => new UpdateEdiOrganizationCommand(c.Id, c.Name, c.Email));

        //Edi model
        profile.CreateMap<EdiModelViewModel, RegisterEdiModelCommand>()
            .ConstructUsing(c => new RegisterEdiModelCommand(c.Org_Id, c.Title, c.SegmentTerminator,
                c.SubElementSeparator, c.ElementSeparator, c.Enabled));
        profile.CreateMap<EdiModelViewModel, UpdateEdiModelCommand>()
            .ConstructUsing(c => new UpdateEdiModelCommand(c.Id, c.Org_Id, c.Title, c.SegmentTerminator,
                c.SubElementSeparator, c.ElementSeparator, c.Enabled));

        //Edi profile
        profile.CreateMap<EdiProfileViewModel, RegisterEdiProfileCommand>()
            .ConstructUsing(
                c => new RegisterEdiProfileCommand(c.Customer_Id, c.Model_Id, c.Title, c.Payload, c.Enabled));
        profile.CreateMap<EdiProfileViewModel, UpdateEdiProfileCommand>()
            .ConstructUsing(c =>
                new UpdateEdiProfileCommand(c.Id, c.Customer_Id, c.Model_Id, c.Title, c.Payload, c.Enabled));

        //Edi Connection
        profile.CreateMap<EdiConnectionViewModel, RegisterEdiConnectionCommand>()
            .ConstructUsing(c => new RegisterEdiConnectionCommand(c.Customer_Id, c.Model_Id, c.Org_Id, c.Profile_Id,
                c.Ftp_Hostname, c.Ftp_Username, c.Ftp_Password,c.Ftp_Port,c.File_Type));
        profile.CreateMap<EdiConnectionViewModel, UpdateEdiConnectionCommand>()
            .ConstructUsing(c => new UpdateEdiConnectionCommand(c.Id, c.Customer_Id, c.Model_Id, c.Org_Id, c.Profile_Id,
                c.Ftp_Hostname, c.Ftp_Username, c.Ftp_Password,c.Ftp_Port,c.File_Type));

        //Edi Segment
        profile.CreateMap<EdiSegmentViewModel, RegisterEdiSegmentCommand>()
            .ConstructUsing(c => new RegisterEdiSegmentCommand(c.Id, c.Model_Id, c.Title, c.Description));
        profile.CreateMap<EdiSegmentViewModel, UpdateEdiSegmentCommand>()
            .ConstructUsing(c => new UpdateEdiSegmentCommand(c.Id, c.Model_Id, c.Title, c.Description));

        //Edi Variable
        profile.CreateMap<EdiVariableViewModel, RegisterEdiVariableCommand>()
            .ConstructUsing(c => new RegisterEdiVariableCommand(c.Id, c.Title, c.Description, c.Placeholder));
        profile.CreateMap<EdiVariableViewModel, UpdateEdiVariableCommand>()
            .ConstructUsing(c => new UpdateEdiVariableCommand(c.Id, c.Title, c.Description, c.Placeholder));

        //Edi Document
        profile.CreateMap<EdiDocumentViewModel, RegisterEdiDocumentCommand>()
            .ConstructUsing(c => new RegisterEdiDocumentCommand(c.Id, c.Customer_Id, c.Title, c.EdiPayload,
                c.DocumentPayload, c.Hedentid, c.IsProcessed, c.IsSent));
        profile.CreateMap<EdiDocumentViewModel, UpdateEdiDocumentCommand>()
            .ConstructUsing(c => new UpdateEdiDocumentCommand(c.Id, c.Customer_Id, c.Title, c.EdiPayload,
                c.DocumentPayload, c.Hedentid, c.IsProcessed, c.IsSent));

        //Edi document receiver
        profile.CreateMap<EdiDocumentReceiverViewModel, ReceivedEdiDocumentCommand>()
            .ConstructUsing(c => new ReceivedEdiDocumentCommand(Guid.Empty, c.Customer_Id, c.Title, "",
                c.DocumentPayload, c.Hedentid, false, false));

        //Edi Credit
        profile.CreateMap<RegisterEdiCreditViewModel, RegisterEdiCreditCommand>()
            .ConstructUsing(c =>
                new RegisterEdiCreditCommand(new EdiCredit(Guid.NewGuid(), c.CustomerId, c.Amount, DateTime.Now)));
        profile.CreateMap<UpdateEdiCreditViewModel, UpdateEdiCreditCommand>()
            .ConstructUsing(c =>
                new UpdateEdiCreditCommand(new EdiCredit(c.Id, c.CustomerId, c.CreditAmount, DateTime.Now)));
    }
}