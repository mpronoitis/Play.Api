using FluentValidation.Results;
using NetDevPack.Mediator;
using Newtonsoft.Json;
using Play.Application.Whmcs.Interfaces;
using Play.Domain.Whmcs.Commands;
using Play.Domain.Whmcs.Models;
using Play.Domain.Whmcs.ResponseModels;
using Play.Whmcs.Connector.Core;

namespace Play.Application.Whmcs.Services;

public class WhmcsClientService : IWhmcsClientService
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly WhmcsApi _whmcsApi;

    public WhmcsClientService(WhmcsApi whmcsApi, IMediatorHandler mediatorHandler)
    {
        _whmcsApi = whmcsApi;
        _mediatorHandler = mediatorHandler;
    }

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
    public async Task<List<WhmcsClient>> GetClients(int limitstart = 0, int limitnum = 25, string sorting = "ASC",
        string status = "Active", string search = "", string orderby = "id")
    {
        var whmcsClients =
            await _whmcsApi.ClientCommands.GetClients(limitstart, limitnum, sorting, status, search, orderby);
        //parse json to WhmcsGetClientsResponseModel
        var whmcsGetClientsResponseModel = JsonConvert.DeserializeObject<WhmcsGetClientsResponseModel>(whmcsClients);
        //return clients
        return whmcsGetClientsResponseModel?.whmcsapi.clients.client ??
               throw new InvalidOperationException("No clients found");
    }

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
    public async Task<WhmcsDomain[]> GetClientsDomains(int limitstart = 0, int limitnum = 25, int clientid = 0,
        int domainid = 0, string domain = "")
    {
        var whmcsClientsDomains =
            await _whmcsApi.ClientCommands.GetClientDomains(limitstart, limitnum, clientid, domainid, domain);
        var model = WhmcsGetDomainsResponseModel.FromJson(whmcsClientsDomains);
        return model.Whmcsapi.Domains?.Domain ?? Array.Empty<WhmcsDomain>();
    }

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
    public async Task<WhmcsPurchasedProduct[]> GetClientsProducts(int limitstart = 0, int limitnum = 25,
        int clientid = 0,
        int serviceid = 0, int pid = 0, string domain = "", string username2 = "")
    {
        var whmcsClientsProducts =
            await _whmcsApi.ClientCommands.GeClientsProducts(limitstart, limitnum, clientid, serviceid, pid, domain,
                username2);
        //whmcsClientProducts is a json string , before parsing it we want to check if the Product array is not null

        var model = WhmcsGetPurchasedProductsResponseModel.FromJson(whmcsClientsProducts);
        //if model has no Product return empty array
        return model.Whmcsapi.Products?.Product ?? Array.Empty<WhmcsPurchasedProduct>();
    }

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
    public async Task<string> GetClientsDetails(int clientid = 0, string email = "", bool status = false)
    {
        var whmcsClientsDetails = await _whmcsApi.ClientCommands.GetClientsDetails(clientid, email, status);
        return whmcsClientsDetails;
    }

    /// <summary>
    ///     AddClient
    /// </summary>
    /// <param name="client">
    ///     <see cref="WhmcsAddClient" />
    ///     <returns>ValidationResult</returns>
    public async Task<ValidationResult> AddClient(WhmcsAddClient client)
    {
        var command = new AddWhmcsClientCommand(client);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     CloseClient
    /// </summary>
    /// <param name="clientid">The ID of the client to close.Required</param>
    /// return json string with result with status and clientid with id of closed client
    public async Task<string> CloseClient(int clientid)
    {
        return await _whmcsApi.ClientCommands.CloseClient(clientid);
    }

    /// <summary>
    ///     Get Clients that have demo packages that have exceeded the demo period (1 month)
    /// </summary>
    /// <returns>Array of clients</returns>
    public async Task<WhmcsClient[]> GetClientsWithExpiredDemoPackages()
    {
        //get all clients
        var clients = await GetClients();
        //get all packages for each client , if any package has price 0 , status active and regDate is over a month ago , add client to list
        var clientsWithExpiredDemoPackages = new List<WhmcsClient>();
        foreach (var client in clients)
        {
            //convert client id to int
            var clientId = int.Parse(client.id);
            var clientProducts = await GetClientsProducts(0, 100, clientId);
            var clientHasExpiredDemoPackage = clientProducts.Any(product =>
                product.Regdate < DateTime.Now.AddMonths(-1) && product.Status == "Active" &&
                product.Recurringamount == "0.00");
            if (clientHasExpiredDemoPackage)
                clientsWithExpiredDemoPackages.Add(client);
        }

        //drop clients with id 7 and 20 (play and pcs)
        clientsWithExpiredDemoPackages.RemoveAll(client => client.id is "7" or "20");
        return clientsWithExpiredDemoPackages.ToArray();
    }
}