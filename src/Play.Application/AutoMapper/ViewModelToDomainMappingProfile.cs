using Play.Application.Contact.AutoMapper;
using Play.Application.Contracting.AutoMapper;
using Play.Application.Core.AutoMapper;
using Play.Application.Edi.AutoMapper;
using Play.Application.Epp.AutoMapper;

namespace Play.Application.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        //Epp
        EppViewModelToDomainMapping.RegisterMappings(this);

        //Contacting
        ContactingViewModelToDomainMapping.RegisterMappings(this);

        //Core
        CoreViewModelToDomainMapping.RegisterMappings(this);

        //Edi
        EdiViewModelToDomainMapping.RegisterMappings(this);

        //Contracting
        ContractingViewModelToDomainMapping.RegisterMappings(this);
    }
}