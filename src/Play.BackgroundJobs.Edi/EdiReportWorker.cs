using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using Play.BackgroundJobs.Edi.Interfaces;
using Play.Domain.Core.Interfaces;
using Play.Domain.Edi.Interfaces;

namespace Play.BackgroundJobs.Edi;

//        ..--""|
//        |     |
//        | .---'
//  (\-.--| |-----------.
// / \) \ | |            \
// |:.  | | |             |                 This worker generates EDI reports for EDI Customers
// |:.  | |o| E - M A I L |
// |:.  | `"`             |
// |:.  |_  __   __ _  __ /
// `""""`""""|=`|"""""""`
//           |=_|
//           |= |
public class EdiReportWorker : IEdiReportWorker
{
    private readonly IEdiDocumentRepository _ediDocumentRepository;
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly ILogger<EdiReportWorker> _logger;
    private readonly IFluentEmailFactory _mailer;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUserRepository _userRepository;

    public EdiReportWorker(IFluentEmailFactory mailer, IEmailTemplateRepository emailTemplateRepository,
        IEdiDocumentRepository ediDocumentRepository, IUserRepository userRepository,
        IUserProfileRepository userProfileRepository, ILogger<EdiReportWorker> logger)
    {
        _mailer = mailer;
        _emailTemplateRepository = emailTemplateRepository;
        _ediDocumentRepository = ediDocumentRepository;
        _userRepository = userRepository;
        _userProfileRepository = userProfileRepository;
        _logger = logger;
    }

    /// <summary>
    ///     responsible for sending a weekly report email to each customer, containing the number of EDI documents that have
    ///     been sent by that customer in the past week.
    ///     It does this by first retrieving all the EDI documents from the _ediDocumentRepository object that were sent in the
    ///     past week and extracting the distinct customer ids from these documents.
    ///     It then iterates over the customer ids,
    ///     retrieves the user and user profile information for each customer from the _userRepository and
    ///     _userProfileRepository objects, respectively, and counts the number of EDI documents sent by that customer.
    ///     It stores this information in a list of tuples. Next, it retrieves the email template for the weekly EDI report
    ///     from the _emailTemplateRepository object and iterates over the list of tuples,
    ///     sending an email to each customer using the _mailer object and the retrieved email template. The email includes the
    ///     customer's name and the number of EDI documents sent by the customer in the past week.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public async Task DoWork()
    {
        try
        {
            //get all documents that were sent in the last week
            var documents =
                await _ediDocumentRepository.GetAllWithDateRangeAsync(DateTime.Now.AddDays(-7), DateTime.Now);
            //get distinct customer_id 
            var ediDocuments = documents.ToList();
            var customerIds = ediDocuments.Select(x => x.Customer_Id).Distinct();
            //get the names ,emails and count of sent docs, of the customers
            var namesAndEmailsAndCount = new List<(string, string, int)>();
            foreach (var customerId in customerIds)
            {
                var user = await _userRepository.GetByIdAsync(customerId) ?? throw new Exception("User not found");
                var profile = await _userProfileRepository.GetByUserId(customerId) ??
                              throw new Exception("User profile not found");
                var count = ediDocuments.Count(x => x.Customer_Id == customerId);

                namesAndEmailsAndCount.Add((profile.FirstName, user.Email, count));
            }

            //get the email template
            var templates = await _emailTemplateRepository.GetByNameAsync("Weekly EDI Report") ??
                            throw new Exception("Email template not found");
            var template = templates.FirstOrDefault();
            //send an email to each customer
            foreach (var (name, email, count) in namesAndEmailsAndCount)
                await _mailer.Create().To(email)
                    .Subject(template.Subject)
                    .UsingTemplate(template.Body, new
                    {
                        Username = name,
                        EdiSent = count
                    })
                    .SendAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in EdiReportWorker");
        }
    }
}