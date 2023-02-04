using Play.Application.Contracting.ViewModels;
using Play.Domain.Contracting.Models;
using Play.Epp.Connector.Models.Contacts;

namespace Play.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        //User
        CreateMap<User, UserViewModel>();
        CreateMap<User, UpdateUserViewModel>();
        CreateMap<User, UpdatePasswordUserViewModel>();
        CreateMap<User, UserForgotPasswordViewModel>();
        CreateMap<User, UpdateRoleUserViewModel>();

        //User Profile
        CreateMap<UserProfile, UserProfileViewModel>();

        //Email Template
        CreateMap<EmailTemplate, EmailTemplateViewModel>();
        CreateMap<EmailTemplate, UpdateEmailTemplateViewModel>();

        //Edi
        CreateMap<EdiOrganization, EdiOrganizationViewModel>();
        CreateMap<EdiModel, EdiModelViewModel>();
        CreateMap<EdiProfile, EdiProfileViewModel>();
        CreateMap<EdiConnection, EdiConnectionViewModel>();
        CreateMap<EdiSegment, EdiSegmentViewModel>();
        CreateMap<EdiVariable, EdiVariableViewModel>();
        CreateMap<EdiDocument, EdiDocumentViewModel>();
        CreateMap<EdiDocument, EdiDocumentReceiverViewModel>();
        CreateMap<EdiCredit, RegisterEdiCreditViewModel>();
        CreateMap<EdiCredit, UpdateEdiCreditViewModel>();

        //Epp
        CreateMap<EppRegisterDomainModel, RegisterEppDomainViewModel>();
        CreateMap<TransferEppDomainViewModel, EppTransferDomainModel>();
        CreateMap<RenewEppDomainViewModel, EppRenewDomainModel>();
        CreateMap<EPPContact, RegisterEppContactViewModel>();

        //Contracting
        CreateMap<Contract, ContractViewModel>();
        CreateMap<Contract, UpdateContractViewModel>();
    }
}