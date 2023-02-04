using Play.Application.Contracting.Interfaces;
using Play.Application.Contracting.ViewModels;

namespace Play.Services.Api.Controllers.Contracting;

[Route("contracting")]
public class ContractController : ApiController
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    /// <summary>
    ///     Get all contracts with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    [HttpGet("all/{page}/{pageSize}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAll(int page, int pageSize)
    {
        try
        {
            var contracts = await _contractService.GetContractsAsync(page, pageSize);
            return CustomResponse(contracts);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Create a new contract
    /// </summary>
    /// <param name="contract">Contract to be created</param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Create([FromBody] ContractViewModel contract)
    {
        try
        {
            var contractCreated = await _contractService.CreateContractAsync(contract);
            return CustomResponse(contractCreated);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Update a contract
    /// </summary>
    /// <param name="contract">Contract to be updated</param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> Update([FromBody] UpdateContractViewModel contract)
    {
        try
        {
            var contractUpdated = await _contractService.UpdateContractAsync(contract);
            return CustomResponse(contractUpdated);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return CustomResponse();
        }
    }
}