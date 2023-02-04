using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Play.Application.Edi.Interfaces;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

/// <summary>
///     Actions Service is responsible for managing actions for the action controller.
///     Actions can be building edi documents, sending edi documents
/// </summary>
public class EdiActionService : IEdiActionService
{
    private readonly IEdiBuilderRepository _builderRepository;
    private readonly ILogger<EdiActionService> _logger;
    private readonly IEdiSendRepository _sendRepository;


    public EdiActionService(IEdiBuilderRepository builderRepository, IEdiSendRepository sendRepository,
        ILogger<EdiActionService> logger)
    {
        _builderRepository = builderRepository;
        _sendRepository = sendRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Function to build all un built edi documents for a given customer id
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    public async Task<ValidationResult> BuildEdiDocuments(Guid customerId)
    {
        var validationResult = new ValidationResult();
        try
        {
            var res = await _builderRepository.BuildUnparsed(customerId);
            if (!res)
                validationResult.Errors.Add(new ValidationFailure("BuildEdiDocuments",
                    "Failed building edi documents"));
        }
        catch (Exception ex)
        {
            validationResult.Errors.Add(new ValidationFailure("BuildEdiDocuments", ex.Message));
            _logger.LogError(ex, "BuildEdiDocuments");
        }

        return validationResult;
    }

    /// <summary>
    ///     Function to send all un sent edi documents for a given customer id
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    public async Task<ValidationResult> SendEdiDocuments(Guid customerId)
    {
        var validationResult = new ValidationResult();
        try
        {
            var res = await _sendRepository.SendUnsentEdiFiles(customerId);
            if (!res)
                validationResult.Errors.Add(new ValidationFailure("SendEdiDocuments", "Failed sending edi documents"));
        }
        catch (Exception ex)
        {
            validationResult.Errors.Add(new ValidationFailure("SendEdiDocuments", ex.Message));
            _logger.LogError(ex, "SendEdiDocuments");
        }

        return validationResult;
    }
}