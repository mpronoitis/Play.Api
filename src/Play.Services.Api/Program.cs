using Microsoft.AspNetCore.RateLimiting;
using Play.BackgroundJobs.Malwarebytes.Interfaces;
using Play.BackgroundJobs.Pylon;
using Play.Services.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


// ConfigureServices

//serilog , will be used for logging in the application , coupled with a SEQ sink for production
builder.UseSerilogSetup();


// WebAPI Config , we want JSON to be case sensitive
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false);

// Setting DBContextss
builder.Services.AddDatabaseConfiguration(builder.Configuration);


// ASP.NET Identity Settings & JWT
builder.Services.AddApiIdentityConfiguration(builder.Configuration);

//Add hangfire configuration
builder.Services.AddHangfireConfig(builder.Configuration);

//Add mailer config
builder.Services.AddMailerConfig(builder.Configuration);

//Add whmcs integration config
builder.Services.AddWhmcs(builder.Configuration);

//Add Epp integration config
builder.Services.AddEppConnector(builder.Configuration);

//Add Mbam integration config
builder.Services.AddMbamConnector(builder.Configuration);

// AutoMapper Settings
builder.Services.AddAutoMapperConfiguration();

// Swagger Config if not in production
if (builder.Environment.IsDevelopment()) builder.Services.AddSwaggerConfiguration();

// Adding MediatR for Domain Events and Notifications
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// .NET Native DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();

// RateLimit Config
builder.Services.AddRateLimitService(builder.Configuration);

//gzip compression
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
    options.EnableForHttps = true;
});

builder.Services.AddHealthChecks();

//RateLimiter Config

builder.Services.AddRateLimiterConfig();

var app = builder.Build();


// Use Hangfire dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]
    {
        new DashboardNoAuthorizationFilter()
    },
    DashboardTitle = "Play.Api Scheduler Dashboard"
});

//set scheduled jobs
//run only if the app is not in development
if (!app.Environment.IsDevelopment())
{
    // BackgroundJob.Enqueue<ITestWorker>(x => x.DoWork());
    // BackgroundJob.Schedule<ITestWorker>(x => x.DoWork(), TimeSpan.FromSeconds(10));
    RecurringJob.AddOrUpdate<IEdiBuilderWorker>(x => x.DoWork(), Cron.Hourly);
    //every 1 hour
    RecurringJob.AddOrUpdate<IEdiSenderWorker>(x => x.DoWork(), Cron.Hourly(15));
    //every day at 5:00 AM build pylon invoices
    RecurringJob.AddOrUpdate<IPylonInvoiceBuilderWorker>(x => x.DoWork(), Cron.Daily(5));
    //every day at 6:00 AM update pylon items
    RecurringJob.AddOrUpdate<PylonItemWorker>(x => x.DoWork(), Cron.Daily(6));
    //every day at 7:00 AM update pylon contacts
    RecurringJob.AddOrUpdate<IPylonContactWorker>(x => x.DoWork(), Cron.Daily(7));
    //every week on monday at 6:00 AM send edi weekly report
    RecurringJob.AddOrUpdate<IEdiReportWorker>(x => x.DoWork(), Cron.Weekly(DayOfWeek.Monday, 6));
    //every week on monday at 5.00 AM send malwarebytes weekly report
    RecurringJob.AddOrUpdate<IMbamReportWorker>(x => x.DoWork(), Cron.Weekly(DayOfWeek.Monday, 5));
}




// Configure

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

//gzip compression
app.UseResponseCompression();

//serilog request logging with options
app.UseCustomSerilogRequestLogging();
app.PushSerilogProperties();

//add NotFoundLoggingMiddleware
app.UseMiddleware<NotFoundLoggingMiddleware>();
//app.UseMiddleware<EdiReceiverAuthMiddleware>();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.WithOrigins("https://app.playsystems.io", "https://manage.playsystems.io", "http://localhost:4200","https://appdev.playsystems.io");
    c.AllowCredentials();
    c.SetIsOriginAllowedToAllowWildcardSubdomains();
    c.SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
});


//enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseRateLimiter();
app.UseHealthChecks("/heartbeat/check");


// Swagger Config if not in production
if (app.Environment.IsDevelopment()) app.UseSwaggerSetup();

app.Run();