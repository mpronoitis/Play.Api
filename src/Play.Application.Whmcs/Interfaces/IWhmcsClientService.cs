using FluentValidation.Results;
using Play.Domain.Whmcs.Models;

namespace Play.Application.Whmcs.Interfaces;

public interface IWhmcsClientService
{
    /// <summary>
    ///     GetClients
    /// </summary>
    /// <param name="limitstart">The offset for the returned client data (default: 0).Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25).Optional</param>
    /// <param name="sorting">The direction to sort the results. ASC or DESC. Default: ASC.Optional</param>
    /// <param name="status">Optional desired Client Status. ‘Active’, ‘Inactive’, or ‘Closed’.Optional</param>
    /// <param name="search">
    ///     The search term to look for at the start of email, firstname, lastname, fullname or
    ///     companyname.Optional
    /// </param>
    /// <param name="orderby">
    ///     The column to order by. id, firstname, lastname, companyname, email, groupid, datecreated,
    ///     status.Optional
    /// </param>
    /// <returns>json</returns>
    Task<List<WhmcsClient>> GetClients(int limitstart = 0, int limitnum = 25, string sorting = "ASC",
        string status = "Active",
        string search = "", string orderby = "id");

    /// <summary>
    ///     GetClientsDomains
    ///     Obtain a list of Client Purchased Domains matching the provided criteria
    /// </summary>
    /// <param name="limitstart">The offset for the returned log data (default: 0).Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25).Optional</param>
    /// <param name="clientid">The client id to obtain the details for. Optional</param>
    /// <param name="domainid">The specific domain id to obtain the details for. Optional</param>
    /// <param name="domain">The specific domain name to obtain the details for. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getclientsdomains/</docs>
    /// <returns>
    ///     json string with result -> The result of the operation: success or error, clientid -> The specific client id
    ///     searched for, domainid -> The specific domain id searched for, totalresults -> The total number of results
    ///     available, startnumber -> The starting number for the returned results, numreturned -> The number of records
    ///     returned, domains -> The domains that match the criteria passed
    /// </returns>
    Task<WhmcsDomain[]> GetClientsDomains(int limitstart = 0, int limitnum = 25, int clientid = 0,
        int domainid = 0, string domain = "");

    /// <summary>
    ///     GetClientsProducts
    ///     Obtain a list of Client Purchased Products matching the provided criteria
    /// </summary>
    /// <param name="limitstart">The offset for the returned log data (default: 0).Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25).Optional</param>
    /// <param name="clientid">The client id to obtain the details for. Optional</param>
    /// <param name="serviceid">The specific service id to obtain the details for. Optional</param>
    /// <param name="pid">The specific product id to obtain the details for. Optional</param>
    /// <param name="domain">The specific domain to obtain the service details for. Optional</param>
    /// <param name="username2">The specific username to obtain the service details for. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getclientsproducts/</docs>
    /// <returns>
    ///     json string result	string	The result of the operation: success or error ,clientid	int	The specific client id
    ///     searched for, serviceid	int	The specific service id searched for, pid	int	The specific product id searched for,
    ///     domain	string	The specific domain searched for, totalresults	int	The total number of results available, startnumber
    ///     int	The starting number for the returned results, numreturned int	The total number of results returned, products
    ///     array	The products returned matching the criteria passed
    /// </returns>
    Task<WhmcsPurchasedProduct[]> GetClientsProducts(int limitstart = 0, int limitnum = 25, int clientid = 0,
        int serviceid = 0, int pid = 0, string domain = "", string username2 = "");

    /// <summary>
    ///     AddClient
    /// </summary>
    /// <param name="client">
    ///     <see cref="WhmcsAddClient" />
    ///     <returns>ValidationResult</returns>
    Task<ValidationResult> AddClient(WhmcsAddClient client);

    /// <summary>
    ///     GetClientsDetails
    ///     Obtain the Clients Details for a specific client
    /// </summary>
    /// <param name="clientid">The client id to obtain the details for. $clientid or $email is required</param>
    /// <param name="email">The email address of the client to obtain the details for. $clientid or $email is required</param>
    /// <param name="status">Also return additional client statistics. Optional,Default: false</param>
    /// <docs>https://developers.whmcs.com/api-reference/getclientsdetails/</docs>
    /// <returns>
    ///     json string with result -> The result of the operation: success or error, client -> The client
    ///     details(array),stats -> The client statistics(array)
    /// </returns>
    Task<string> GetClientsDetails(int clientid = 0, string email = "", bool status = false);

    /// <summary>
    ///     CloseClient
    /// </summary>
    /// <param name="clientid">The ID of the client to close.Required</param>
    /// return json string with result with status and clientid with id of closed client
    Task<string> CloseClient(int clientid);

    /// <summary>
    ///     Get Clients that have demo packages that have exceeded the demo period (1 month)
    /// </summary>
    /// <returns>Array of clients</returns>
    public Task<WhmcsClient[]> GetClientsWithExpiredDemoPackages();
}