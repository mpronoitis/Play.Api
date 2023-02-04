using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetDevPack.Mediator;
using Play.Application.Core.Interfaces;
using Play.Application.Core.Services;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.Services;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Events;
using Play.Domain.Core.Interfaces;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Events;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Pylon.Interfaces;
using Play.Infra.Data.Context;
using Play.Infra.Data.Edi.Repository;
using Play.Infra.Data.Pylon.Repositories;
using Play.Infra.Data.Repository;
using Play.Testing.Setup.Bus;

namespace Play.Testing.Ioc;

/// <summary>
///     A simple IoC container for use in unit tests.
/// </summary>
public static class Bootstraper
{
    public static void AddServices(this IServiceCollection services)
    {
        // Domain Bus (Mediator)
        services.AddScoped<IMediatorHandler, InMemoryBus>();
        //contexts
        services.AddScoped<PlayContext>();
        services.AddScoped<PlayCoreContext>();
        services.AddScoped<PlayPylonContext>();

        //edi repositories
        services.AddScoped<IEdiConnectionRepository, EdiConnectionRepository>();
        services.AddScoped<IEdiModelRepository, EdiModelRepository>();
        services.AddScoped<IEdiOrganizationRepository, EdiOrganizationRepository>();
        services.AddScoped<IEdiProfileRepository, EdiProfileRepository>();
        services.AddScoped<IEdiVariableRepository, EdiVariableRepository>();
        services.AddScoped<IEdiSegmentRepository, EdiSegmentRepository>();
        services.AddScoped<IEdiDocumentRepository, EdiDocumentRepository>();

        //core repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();

        //pylon repositories
        services.AddScoped<IPylonItemRepository, PylonItemRepository>();
        services.AddScoped<IPylonInvoiceRepository, PylonInvoiceRepository>();
        services.AddScoped<IPylonTempContactRepository, PylonTempContactRepository>();

        //logger factory
        services.AddSingleton<ILoggerFactory, LoggerFactory>();

        //ILogger for UserCommandHandler
        services.AddScoped<ILogger<UserCommandHandler>, Logger<UserCommandHandler>>();
        //ILogger<UserEventHandler>
        services.AddScoped<ILogger<UserEventHandler>, Logger<UserEventHandler>>();

        //commands User
        services.AddScoped<IRequestHandler<RegisterUserCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveUserCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserPasswordCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserRoleCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<ForgotPasswordCommand, ValidationResult>, UserCommandHandler>();

        //events User
        services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserRemovedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserPasswordUpdatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<ForgotPasswordEvent>, UserEventHandler>();


        //commands UserProfile
        services.AddScoped<IRequestHandler<RemoveUserProfileCommand, ValidationResult>, UserProfileCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterUserProfileCommand, ValidationResult>, UserProfileCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserProfileCommand, ValidationResult>, UserProfileCommandHandler>();

        //events UserProfile
        services.AddScoped<INotificationHandler<UserProfileUpdatedEvent>, UserProfileEventHandler>();

        //ILogger for EdiDocumentCommandHandler
        services.AddScoped<ILogger<EdiDocumentCommandHandler>, Logger<EdiDocumentCommandHandler>>();

        //commands EdiVariable
        services.AddScoped<IRequestHandler<RegisterEdiVariableCommand, ValidationResult>, EdiVariableCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiVariableCommand, ValidationResult>, EdiVariableCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiVariableCommand, ValidationResult>, EdiVariableCommandHandler>();

        //events EdiVariable
        services.AddScoped<INotificationHandler<EdiVariableRegisteredEvent>, EdiVariableEventHandler>();
        services.AddScoped<INotificationHandler<EdiVariableUpdatedEvent>, EdiVariableEventHandler>();
        services.AddScoped<INotificationHandler<EdiVariableRemovedEvent>, EdiVariableEventHandler>();

        //commands EdiCredit
        services.AddScoped<IEdiCreditService, EdiCreditService>();
        services.AddScoped<IEdiCreditRepository, EdiCreditRepository>();
        //domain - commands - EdiCredit
        services.AddScoped<IRequestHandler<RegisterEdiCreditCommand, ValidationResult>, EdiCreditCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiCreditCommand, ValidationResult>, EdiCreditCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiCreditCommand, ValidationResult>, EdiCreditCommandHandler>();

        //email template
        services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
    }
}