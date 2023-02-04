using AutoMapper;
using Play.Application.Epp.ViewModels;
using Play.Domain.Epp.Commands;
using Play.Domain.Epp.Models;
using Play.Epp.Connector.Models.Contacts;

namespace Play.Application.Epp.AutoMapper;

public class EppViewModelToDomainMapping
{
    public static void RegisterMappings(Profile profile)
    {
        //Epp
        profile.CreateMap<RegisterEppDomainViewModel, RegisterEppDomainCommand>()
            .ConstructUsing(c => new RegisterEppDomainCommand(new EppRegisterDomainModel(c.DomainName, c.Registrant,
                c.Admin, c.Tech, c.Billing, c.Password, c.Period)));
        profile.CreateMap<RegisterEppContactViewModel, RegisterEppContactCommand>()
            .ConstructUsing(c => new RegisterEppContactCommand(new EPPContact(c.Id, c.LocalizedName,
                c.LocalizedOrganization, c.LocalizedStreet, c.LocalizedCity, c.LocalizedState, c.LocalizedPostalCode,
                c.LocalizedCountry, c.InternationalName, c.InternationalOrganization, c.InternationalStreet,
                c.InternationalCity, c.InternationalState, c.InternationalPostalCode, c.InternationalCountry,
                c.VoicePhone, c.FaxPhone, c.Email, c.Password, c.DiscloseFlag)));
        profile.CreateMap<TransferEppDomainViewModel, TransferEppDomainCommand>()
            .ConstructUsing(c =>
                new TransferEppDomainCommand(new EppTransferDomainModel(c.DomainName, c.Password, c.NewPassword,
                    c.ContactId)));
        profile.CreateMap<RenewEppDomainViewModel, RenewEppDomainCommand>()
            .ConstructUsing(c => new RenewEppDomainCommand(new EppRenewDomainModel(c.DomainName, c.Years)));
        profile.CreateMap<RegisterEppNameserverViewModel, RegisterEppNameserverCommand>()
            .ConstructUsing(c =>
                new RegisterEppNameserverCommand(c.Nameserver, c.DomainName));
        profile.CreateMap<RegisterEppNameserversViewModel, RegisterListEppNameserversCommand>()
            .ConstructUsing(c =>
                new RegisterListEppNameserversCommand(c.Domain, c.Nameservers));
    }
}