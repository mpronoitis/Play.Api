using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Play.Domain.Contact.Commands;
using Play.Domain.Contracting.Commands;
using Play.Domain.Core.Commands;
using Play.Domain.Edi.Commands;
using Play.Domain.Epp.Commands;
using Play.Domain.Pylon.Commands;
using Play.Domain.Whmcs.Commands;

namespace Play.Infra.CrossCutting.IoC;

public static class MediatrCommandServices
{
    public static void AddMediatrCommandServices(this IServiceCollection services)
    {
        //domain - commands - user
        services.AddScoped<IRequestHandler<RegisterUserCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveUserCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserPasswordCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<ForgotPasswordCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserRoleCommand, ValidationResult>, UserCommandHandler>();

        //domain - commands - user profile
        services.AddScoped<IRequestHandler<RegisterUserProfileCommand, ValidationResult>, UserProfileCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserProfileCommand, ValidationResult>, UserProfileCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveUserProfileCommand, ValidationResult>, UserProfileCommandHandler>();

        //domain - commands - email templates
        services
            .AddScoped<IRequestHandler<RegisterNewEmailTemplateCommand, ValidationResult>,
                EmailTemplateCommandHandler>();
        services
            .AddScoped<IRequestHandler<UpdateEmailTemplateCommand, ValidationResult>, EmailTemplateCommandHandler>();
        services
            .AddScoped<IRequestHandler<RemoveEmailTemplateCommand, ValidationResult>, EmailTemplateCommandHandler>();

        //domain - commands - EdiDocument
        services.AddScoped<IRequestHandler<ReceivedEdiDocumentCommand, ValidationResult>, EdiDocumentCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterEdiDocumentCommand, ValidationResult>, EdiDocumentCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiDocumentCommand, ValidationResult>, EdiDocumentCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiDocumentCommand, ValidationResult>, EdiDocumentCommandHandler>();

        //domain - commands - EdiOrganization
        services
            .AddScoped<IRequestHandler<RegisterEdiOrganizationCommand, ValidationResult>,
                EdiOrganizationCommandHandler>();
        services
            .AddScoped<IRequestHandler<UpdateEdiOrganizationCommand, ValidationResult>,
                EdiOrganizationCommandHandler>();
        services
            .AddScoped<IRequestHandler<RemoveEdiOrganizationCommand, ValidationResult>,
                EdiOrganizationCommandHandler>();

        //domain - commands - EdiModel
        services.AddScoped<IRequestHandler<RegisterEdiModelCommand, ValidationResult>, EdiModelCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiModelCommand, ValidationResult>, EdiModelCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiModelCommand, ValidationResult>, EdiModelCommandHandler>();

        //domain - commands - EdiProfile
        services.AddScoped<IRequestHandler<RegisterEdiProfileCommand, ValidationResult>, EdiProfileCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiProfileCommand, ValidationResult>, EdiProfileCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiProfileCommand, ValidationResult>, EdiProfileCommandHandler>();

        //domain - commands - EdiConnection
        services
            .AddScoped<IRequestHandler<RegisterEdiConnectionCommand, ValidationResult>, EdiConnectionCommandHandler>();
        services
            .AddScoped<IRequestHandler<UpdateEdiConnectionCommand, ValidationResult>, EdiConnectionCommandHandler>();
        services
            .AddScoped<IRequestHandler<RemoveEdiConnectionCommand, ValidationResult>, EdiConnectionCommandHandler>();

        //domain - commands - EdiSegment
        services.AddScoped<IRequestHandler<RegisterEdiSegmentCommand, ValidationResult>, EdiSegmentCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiSegmentCommand, ValidationResult>, EdiSegmentCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiSegmentCommand, ValidationResult>, EdiSegmentCommandHandler>();

        //domain - commands - EdiVariable
        services.AddScoped<IRequestHandler<RegisterEdiVariableCommand, ValidationResult>, EdiVariableCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiVariableCommand, ValidationResult>, EdiVariableCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiVariableCommand, ValidationResult>, EdiVariableCommandHandler>();

        //domain - commands - EdiCredit
        services.AddScoped<IRequestHandler<RegisterEdiCreditCommand, ValidationResult>, EdiCreditCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEdiCreditCommand, ValidationResult>, EdiCreditCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveEdiCreditCommand, ValidationResult>, EdiCreditCommandHandler>();

        //domain - commands - Pylon
        services
            .AddScoped<IRequestHandler<RegisterPylonInvoiceCommand, ValidationResult>, PylonInvoiceCommandHandler>();
        services.AddScoped<IRequestHandler<RemovePylonInvoiceCommand, ValidationResult>, PylonInvoiceCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterPylonItemCommand, ValidationResult>, PylonItemCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterListPylonItemCommand, ValidationResult>, PylonItemCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveAllPylonItemsCommand, ValidationResult>, PylonItemCommandHandler>();
        services
            .AddScoped<IRequestHandler<RegisterPylonContactCommand, ValidationResult>, PylonContactCommandHandler>();
        services
            .AddScoped<IRequestHandler<RegisterListPylonContactCommand, ValidationResult>,
                PylonContactCommandHandler>();
        services
            .AddScoped<IRequestHandler<RemoveAllPylonContactCommand, ValidationResult>, PylonContactCommandHandler>();

        //domain - commands - ContactRequest
        services
            .AddScoped<IRequestHandler<RegisterContactRequestCommand, ValidationResult>,
                ContactRequestCommandHandler>();

        //domain - commands - Whmcs
        services.AddScoped<IRequestHandler<AddWhmcsClientCommand, ValidationResult>, WhmcsAddClientCommandHandler>();
        services.AddScoped<IRequestHandler<AddWhmcsOrderCommand, ValidationResult>, WhmcsAddOrderCommandHandler>();
        services
            .AddScoped<IRequestHandler<AcceptWhmcsOrderCommand, ValidationResult>, WhmcsAcceptOrderCommandHandler>();

        //domain - commands - Epp
        services.AddScoped<IRequestHandler<RegisterEppDomainCommand, ValidationResult>, EppDomainCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterEppContactCommand, ValidationResult>, EppContactCommandHandler>();
        services.AddScoped<IRequestHandler<TransferEppDomainCommand, ValidationResult>, EppDomainCommandHandler>();
        services.AddScoped<IRequestHandler<RenewEppDomainCommand, ValidationResult>, EppDomainCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEppContactCommand, ValidationResult>, EppContactCommandHandler>();
        services
            .AddScoped<IRequestHandler<RegisterEppNameserverCommand, ValidationResult>, EppNameserverCommandHandler>();
        services
            .AddScoped<IRequestHandler<RemoveAllEppNameserversCommand, ValidationResult>,
                EppNameserverCommandHandler>();
        services
            .AddScoped<IRequestHandler<RegisterListEppNameserversCommand, ValidationResult>,
                EppNameserverCommandHandler>();

        //contracting
        services.AddScoped<IRequestHandler<RegisterContractCommand, ValidationResult>, ContractCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateContractCommand, ValidationResult>, ContractCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveContractCommand, ValidationResult>, ContractCommandHandler>();
    }
}