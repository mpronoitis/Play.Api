using AutoMapper;
using Play.Application.Contact.ViewModels;
using Play.Domain.Contact.Commands;

namespace Play.Application.Contact.AutoMapper;

public class ContactingViewModelToDomainMapping
{
    public static void RegisterMappings(Profile profile)
    {
        profile.CreateMap<ContactRequestViewModel, ContactRequestCommand>()
            .ConstructUsing(c => new RegisterContactRequestCommand(c.Email, c.Subject, c.Message, c.PhoneNumber));
    }
}