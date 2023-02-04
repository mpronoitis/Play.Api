using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Core.Events;
using Play.Domain.Edi.Events;

namespace Play.Infra.CrossCutting.IoC;

public static class EventHandlerServices
{
    public static void AddEventHandlerServices(this IServiceCollection services)
    {
        //domain - events - user
        services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserRemovedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserPasswordUpdatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<ForgotPasswordEvent>, UserEventHandler>();

        //domain - events - user profile
        services.AddScoped<INotificationHandler<UserProfileUpdatedEvent>, UserProfileEventHandler>();

        //domain - events - EdiDocument
        services.AddScoped<INotificationHandler<EdiDocumentRegisteredEvent>, EdiDocumentEventHandler>();
        services.AddScoped<INotificationHandler<EdiDocumentUpdatedEvent>, EdiDocumentEventHandler>();
        services.AddScoped<INotificationHandler<EdiDocumentRemovedEvent>, EdiDocumentEventHandler>();
        services.AddScoped<INotificationHandler<EdiDocumentReceivedEvent>, EdiDocumentEventHandler>();

        //domain - events - EdiOrganization
        services.AddScoped<INotificationHandler<EdiOrganizationRegisteredEvent>, EdiOrganizationEventHandler>();
        services.AddScoped<INotificationHandler<EdiOrganizationUpdatedEvent>, EdiOrganizationEventHandler>();
        services.AddScoped<INotificationHandler<EdiOrganizationRemovedEvent>, EdiOrganizationEventHandler>();

        //domain - events - EdiModel
        services.AddScoped<INotificationHandler<EdiModelRegisteredEvent>, EdiModelEventHandler>();
        services.AddScoped<INotificationHandler<EdiModelUpdatedEvent>, EdiModelEventHandler>();
        services.AddScoped<INotificationHandler<EdiModelRemovedEvent>, EdiModelEventHandler>();

        //domain - events - EdiProfile
        services.AddScoped<INotificationHandler<EdiProfileRegisteredEvent>, EdiProfileEventHandler>();
        services.AddScoped<INotificationHandler<EdiProfileUpdatedEvent>, EdiProfileEventHandler>();
        services.AddScoped<INotificationHandler<EdiProfileRemovedEvent>, EdiProfileEventHandler>();

        //domain - events - EdiConnection
        services.AddScoped<INotificationHandler<EdiConnectionRegisteredEvent>, EdiConnectionEventHandler>();
        services.AddScoped<INotificationHandler<EdiConnectionUpdatedEvent>, EdiConnectionEventHandler>();
        services.AddScoped<INotificationHandler<EdiConnectionRemovedEvent>, EdiConnectionEventHandler>();

        //domain - events - EdiSegment
        services.AddScoped<INotificationHandler<EdiSegmentRegisteredEvent>, EdiSegmentEventHandler>();
        services.AddScoped<INotificationHandler<EdiSegmentUpdatedEvent>, EdiSegmentEventHandler>();
        services.AddScoped<INotificationHandler<EdiSegmentRemovedEvent>, EdiSegmentEventHandler>();

        //domain - events - EdiVariable
        services.AddScoped<INotificationHandler<EdiVariableRegisteredEvent>, EdiVariableEventHandler>();
        services.AddScoped<INotificationHandler<EdiVariableUpdatedEvent>, EdiVariableEventHandler>();
        services.AddScoped<INotificationHandler<EdiVariableRemovedEvent>, EdiVariableEventHandler>();
        
        //domain - events - EdiCredit
        services.AddScoped<INotificationHandler<EdiCreditRegisteredEvent>, EdiCreditEventHandler>();
        services.AddScoped<INotificationHandler<EdiCreditUpdatedEvent>, EdiCreditEventHandler>();
        services.AddScoped<INotificationHandler<EdiCreditRemovedEvent>, EdiCreditEventHandler>();
    }
}