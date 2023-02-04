using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using Play.Application._20i.Interfaces;
using Play.Application._20i.Services;
using Play.Application.Contact.Interfaces;
using Play.Application.Contact.Services;
using Play.Application.Contracting.Interfaces;
using Play.Application.Contracting.Services;
using Play.Application.Core.Interfaces;
using Play.Application.Core.Services;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.Services;
using Play.Application.Epp.Interfaces;
using Play.Application.Epp.Services;
using Play.Application.EventSourcing.Interfaces;
using Play.Application.EventSourcing.Services;
using Play.Application.Kuma.Interfaces;
using Play.Application.Kuma.Services;
using Play.Application.Malwarebytes.Interfaces;
using Play.Application.Malwarebytes.Services;
using Play.Application.Pylon.Interfaces;
using Play.Application.Pylon.Services;
using Play.Application.Whmcs.Interfaces;
using Play.Application.Whmcs.Services;
using Play.BackgroundJobs.Edi;
using Play.BackgroundJobs.Edi.Interfaces;
using Play.BackgroundJobs.Malwarebytes;
using Play.BackgroundJobs.Malwarebytes.Interfaces;
using Play.BackgroundJobs.Pylon;
using Play.BackgroundJobs.Pylon.Interfaces;
using Play.Domain.Contracting.Interfaces;
using Play.Domain.Core.Interfaces;
using Play.Domain.Edi.Interfaces;
using Play.Domain.EventSourcing.Events;
using Play.Domain.EventSourcing.Interfaces;
using Play.Domain.Kuma.Interfaces;
using Play.Domain.Pylon.Interfaces;
using Play.Infra.CrossCutting.Bus;
using Play.Infra.Data.Context;
using Play.Infra.Data.Contracting.Repositories;
using Play.Infra.Data.Edi.Repository;
using Play.Infra.Data.EventSourcing;
using Play.Infra.Data.Kuma.Repository;
using Play.Infra.Data.Pylon.Repositories;
using Play.Infra.Data.Repository;
using PylonDatabaseHandler;
using TwentyI_dotnet;
using TwentyI_dotnet.Interfaces;

namespace Play.Infra.CrossCutting.IoC;

public static class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        //add http client factory
        services.AddHttpClient();
        // Domain Bus (Mediator)
        services.AddScoped<IMediatorHandler, InMemoryBus>();
        //Application - Core
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        //Application - Edi
        services.AddScoped<IEdiOrganizationService, EdiOrganizationService>();
        services.AddScoped<IEdiModelService, EdiModelService>();
        services.AddScoped<IEdiProfileService, EdiProfileService>();
        services.AddScoped<IEdiConnectionService, EdiConnectionService>();
        services.AddScoped<IEdiSegmentService, EdiSegmentService>();
        services.AddScoped<IEdiVariableService, EdiVariableService>();
        services.AddScoped<IEdiDocumentService, EdiDocumentService>();
        services.AddScoped<IEdiActionService, EdiActionService>();
        services.AddScoped<IEdiCreditService, EdiCreditService>();

        //Application - Contact
        services.AddScoped<IContactRequestService, ContactRequestService>();

        // Infra - Data
        services.AddScoped<PlayContext>();
        services.AddScoped<PlayCoreContext>();
        services.AddScoped<PlayPylonContext>();
        services.AddScoped<PlayEventStoreContext>();
        //Infra - Data - Core
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        // Infra - Data - Edi
        services.AddScoped<IEdiOrganizationRepository, EdiOrganizationRepository>();
        services.AddScoped<IEdiModelRepository, EdiModelRepository>();
        services.AddScoped<IEdiProfileRepository, EdiProfileRepository>();
        services.AddScoped<IEdiConnectionRepository, EdiConnectionRepository>();
        services.AddScoped<IEdiSegmentRepository, EdiSegmentRepository>();
        services.AddScoped<IEdiVariableRepository, EdiVariableRepository>();
        services.AddScoped<IEdiDocumentRepository, EdiDocumentRepository>();
        services.AddScoped<IEdiBuilderRepository, EdiBuilderRepository>();
        services.AddScoped<IEdiSendRepository, EdiSendRepository>();
        services.AddScoped<IEdiReportRepository, EdiReportRepository>();
        services.AddScoped<IEdiCreditRepository, EdiCreditRepository>();
        
        // Infra - Data - EventSourcing Repository
        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped<IEventStore, EventStore>();
        
        //EventSourcing Services
        services.AddScoped<IEventSourcingService, EventSourcingService>();

        //pylon
        services.AddScoped<IPylonDatabase, PylonDatabase>();

        //pylon repositories
        services.AddScoped<IPylonHeContactRepository, PylonHeContactRepository>();
        services.AddScoped<IPylonTraderRepository, PylonTraderRepository>();
        services.AddScoped<IPylonSysRepository, PylonSysRepository>();
        services.AddScoped<IPylonCustomerRepository, PylonCustomerRepository>();
        services.AddScoped<IPylonCentlineRepository, PylonCentlineRepository>();
        services.AddScoped<IPylonCommercialEntriesRepository, PylonCommercialEntriesRepository>();
        services.AddScoped<IPylonDocentriesRepository, PylonDocentriesRepository>();
        services.AddScoped<IPylonPaymentMethodRepository, PylonPaymentMethodRepository>();
        services.AddScoped<IPylonMeasurementUnitRepository, PylonMeasurementUnitRepository>();
        services.AddScoped<IPylonHeItemRepository, PylonHeItemRepository>();
        services.AddScoped<IPylonDocseriesRepository, PylonDocseriesRepository>();
        services.AddScoped<IPylonDentEipInfoRepository, PylonDentEipInfoRepository>();
        services.AddScoped<IPylonInvoiceRepository, PylonInvoiceRepository>();
        services.AddScoped<IPylonSessionRepository, PylonSessionRepository>();
        services.AddScoped<IPylonItemRepository, PylonItemRepository>();
        services.AddScoped<IPylonTempContactRepository, PylonTempContactRepository>();

        //pylon services
        services.AddScoped<IPylonInvoiceService, PylonInvoiceService>();
        services.AddScoped<IPylonInvoiceBuilderService, PylonInvoiceBuilderService>();
        services.AddScoped<IPylonLdifService, PylonLdifService>();
        services.AddScoped<IPylonSysService, PylonSysService>();
        services.AddScoped<IPylonSessionService, PylonSessionService>();
        services.AddScoped<IPylonHeContactService, PylonHeContactService>();
        services.AddScoped<IPylonItemMigrationService, PylonItemMigrationService>();
        services.AddScoped<IPylonItemService, PylonItemService>();
        services.AddScoped<IPylonContactMigrationService, PylonContactMigrationService>();
        services.AddScoped<IPylonContactService, PylonContactService>();
        services.AddScoped<IPylonDocEntriesService, PylonDocEntriesService>();
        services.AddScoped<IPylonCommercialEntriesService, PylonCommercialEntriesService>();

        //20i services
        services.AddScoped<ITwentyDomainService, TwentyDomainService>();
        services.AddScoped<ITwentyPackageService, TwentyPackageService>();
        services.AddScoped<ITwentyResellerService, TwentyResellerService>();

        //Background Workers
        services.AddScoped<IEdiBuilderWorker, EdiBuilderWorker>();
        services.AddScoped<IEdiSenderWorker, EdiSenderWorker>();
        services.AddScoped<IEdiReportWorker, EdiReportWorker>();
        //Background Workers - Pylon
        services.AddScoped<IPylonInvoiceBuilderWorker, PylonInvoiceBuilderWorker>();
        services.AddScoped<IPylonContactWorker, PylonContactWorker>();
        services.AddScoped<IMbamReportWorker, MbamReportWorker>();


        //20i handler
        var twentiApiService = new TwentyIApi("c27f320ba5a963d22", new HttpClient());
        services.AddSingleton<ITwentyIApi>(twentiApiService);

        //kuma
        services.AddScoped<IKumaNotificationRepository, KumaNotificationRepository>();
        services.AddScoped<IKumaNotificationService, KumaNotificationService>();


        //whmcs - services
        services.AddScoped<IWhmcsClientService, WhmcsClientService>();
        services.AddScoped<IWhmcsSystemService, WhmcsSystemService>();
        services.AddScoped<IWhmcsOrderService, WhmcsOrderService>();

        //epp - services
        services.AddScoped<IEppDomainService, EppDomainService>();
        services.AddScoped<IEppContactService, EppContactService>();
        services.AddScoped<IEppAccountRegistrantService, EppAccountRegistrantService>();

        //mbam - services
        services.AddScoped<IMalwarebytesUserService, MalwarebytesUserService>();
        services.AddScoped<IMalwarebytesSiteService, MalwarebytesSiteService>();
        services.AddScoped<IMalwarebytesDetectionService, MalwarebytesDetectionService>();

        //contracting - repositories
        services.AddScoped<IContractRepository, ContractRepository>();

        //contracting - services
        services.AddScoped<IContractService, ContractService>();

        //add mediatr command services
        services.AddMediatrCommandServices();

        //add mediatr event services
        services.AddEventHandlerServices();
    }
}