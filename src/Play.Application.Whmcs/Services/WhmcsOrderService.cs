using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Whmcs.Interfaces;
using Play.Domain.Whmcs.Commands;
using Play.Domain.Whmcs.Models;
using Play.Domain.Whmcs.ResponseModels;
using Play.Whmcs.Connector.Core;

namespace Play.Application.Whmcs.Services;

public class WhmcsOrderService : IWhmcsOrderService
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly WhmcsApi _whmcsApi;


    public WhmcsOrderService(WhmcsApi whmcsApi, IMediatorHandler mediatorHandler)
    {
        _whmcsApi = whmcsApi;
        _mediatorHandler = mediatorHandler;
    }

    /// <summary>
    ///     GetOrders
    ///     Obtain orders matching the passed criteria
    /// </summary>
    /// <param name="limitstart">The offset for the returned order data (default: 0). Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25). Optional</param>
    /// <param name="id">Find orders for a specific id. Optional</param>
    /// <param name="userid">Find orders for a specific userid. Optional</param>
    /// <param name="requestorId">Find orders for a specific requestor_id. Optional</param>
    /// <param name="status">Find orders for a specific status. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getorders/</docs>
    /// <returns>
    ///     json string with result(string) wtih The result of the operation: success or error,totalresults(int) wtih The
    ///     total number of results found, startnumber(int) The starting number for the returned results, numreturned(int) The
    ///     number of results returned, orders(array) An array of orders matching the criteria passed
    /// </returns>
    public async Task<WhmcsOrder[]> GetOrders(int limitstart = 0, int limitnum = 25, int id = 0, int userid = 0,
        int requestorId = 0, string status = "")
    {
        var result = await _whmcsApi.OrderCommands.GetOrders(limitstart, limitnum, id, userid, requestorId, status);
        var obj = WhmcsGetOrdersResponseModel.FromJson(result);
        //return clients
        return obj.Whmcsapi.Orders.Order;
    }

    /// <summary>
    ///     GetProducts
    ///     Retrieve configured products matching provided criteria
    /// </summary>
    /// <param name="pid">
    ///     The product ID to retrieve. Obtain a specific product id configuration. Can be a list of ids comma
    ///     separated Optional
    /// </param>
    /// <param name="gid">Retrieve products in a specific group id. Optional</param>
    /// <param name="module">Retrieve products utilising a specific module. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getproducts/</docs>
    /// <returns>
    ///     json string with result of operation,totalresults(int) The total number of results available,startnumber(int)
    ///     The starting number for the returned results,numreturned(int) The number of results returned,products(array) An
    ///     array of products matching the criteria passed
    /// </returns>
    public async Task<WhmcsProduct[]> GetProducts(int pid = 0, int gid = 0, string module = "")
    {
        var result = await _whmcsApi.OrderCommands.GetProducts(pid, gid, module);
        var obj = WhmcsGetProductsResponseModel.FromJson(result);
        //return products
        return obj.Whmcsapi.Products.Product;
    }

    /// <summary>
    ///     AddOrder
    ///     Add an order to the system
    /// </summary>
    /// <param name="whmcsOrder">The order to add <see cref="WhmcsAddOrder" /></param>
    public async Task<ValidationResult> AddOrder(WhmcsAddOrder whmcsOrder)
    {
        var command = new AddWhmcsOrderCommand(whmcsOrder);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     Accept order
    ///     Accept an order
    /// </summary>
    /// <param name="acceptOrderModel">The order to accept <see cref="WhmcsAcceptOrder" /></param>
    public async Task<ValidationResult> AcceptOrder(WhmcsAcceptOrder acceptOrderModel)
    {
        var command = new AcceptWhmcsOrderCommand(acceptOrderModel);
        return await _mediatorHandler.SendCommand(command);
    }
}