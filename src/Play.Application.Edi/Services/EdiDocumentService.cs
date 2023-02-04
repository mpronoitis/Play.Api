using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

public class EdiDocumentService : IEdiDocumentService
{
    private readonly IEdiDocumentRepository _ediDocumentRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EdiDocumentService(IEdiDocumentRepository ediDocumentRepository, IMapper mapper,
        IMediatorHandler mediatorHandler)
    {
        _ediDocumentRepository = ediDocumentRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    //get by id async
    public async Task<EdiDocumentViewModel> GetByIdAsync(Guid id)
    {
        var ediDocument = await _ediDocumentRepository.GetByIdAsync(id);
        return _mapper.Map<EdiDocumentViewModel>(ediDocument);
    }

    //get all by customer id with paging
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAllWithPaginationByCustomerIdAsync(Guid customerId,
        int page = 1, int pageSize = 10)
    {
        //var ediDocuments =
        //await _ediDocumentRepository.GetAllWithPaginationByCustomerIdAsync(customerId, page, pageSize);
        var ediDocuments =
            await _ediDocumentRepository.GetAllWithPaginationByCustomerIdAsync(customerId, page, pageSize);
        return _mapper.Map<IEnumerable<EdiDocumentViewModel>>(ediDocuments);
    }

    //get all with pagination without customer id
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAllWithPaginationAsync(int page = 1, int pageSize = 10)
    {
        //var ediDocuments = await _ediDocumentRepository.GetAllWithPaginationAsync(page, pageSize);
        var ediDocuments = await _ediDocumentRepository.GetAllWithPaginationAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiDocumentViewModel>>(ediDocuments);
    }

    //get total count by customer id 
    public async Task<int> GetTotalCountByCustomerIdAsync(Guid customerId)
    {
        return await _ediDocumentRepository.GetTotalCountByCustomerIdAsync(customerId);
    }
    
    //get all with no payloads and pagination and customer id
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAllWithNoPayloadsAndPaginationByCustomerIdAsync(Guid customerId, int page = 1, int pageSize = 10)
    {
        var ediDocuments = await _ediDocumentRepository.GetAllWithNoPayloadsAndCustomerIdAsync(customerId, page, pageSize);
        return _mapper.Map<IEnumerable<EdiDocumentViewModel>>(ediDocuments);
    }
    
    //get all with no payloads and pagination
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAllWithNoPayloadsAndPaginationAsync(int page = 1, int pageSize = 10)
    {
        var ediDocuments = await _ediDocumentRepository.GetAllWithNoPayloadsAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiDocumentViewModel>>(ediDocuments);
    }

    //commands
    public async Task<ValidationResult> Register(EdiDocumentViewModel ediDocumentViewModel)
    {
        var registerCommand = _mapper.Map<RegisterEdiDocumentCommand>(ediDocumentViewModel);
        return await _mediatorHandler.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> Receive(EdiDocumentReceiverViewModel ediDocumentViewModel)
    {
        var receiveCommand = _mapper.Map<ReceivedEdiDocumentCommand>(ediDocumentViewModel);
        var res = await _mediatorHandler.SendCommand(receiveCommand);
        return res;
    }

    public async Task<ValidationResult> Update(EdiDocumentViewModel ediDocumentViewModel)
    {
        var updateCommand = _mapper.Map<UpdateEdiDocumentCommand>(ediDocumentViewModel);
        return await _mediatorHandler.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveEdiDocumentCommand(id);
        return await _mediatorHandler.SendCommand(removeCommand);
    }

    /// <summary>
    ///     Get total count by customer id and date range
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<int> GetTotalCountByCustomerIdAndDateRangeAsync(Guid customerId, DateTime startDate,
        DateTime endDate)
    {
        return await _ediDocumentRepository.GetTotalCountByCustomerIdAndDateRangeAsync(customerId, startDate, endDate);
    }

    /// <summary>
    ///     Get total count
    /// </summary>
    public async Task<int> GetTotalCountAsync()
    {
        return await _ediDocumentRepository.GetTotalCountAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}