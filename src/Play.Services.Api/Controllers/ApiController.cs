namespace Play.Services.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    private readonly ICollection<string> _errors = new List<string>();

    /// <summary>
    ///     Custom response returns 200 OK if operation is valid, otherwise returns 400 Bad Request
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    protected ActionResult CustomResponse(object result = null)
    {
        if (IsOperationValid()) return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", _errors.ToArray() }
        }));
    }

    /// <summary>
    ///     Custom response returns 200 OK if operation is valid, otherwise returns 400 Bad Request
    ///     with custom error messages in the response body
    /// </summary>
    /// <param name="modelState"> The ModelStateDictionary to be validated </param>
    /// <returns>
    ///     <see cref="ActionResult" />
    /// </returns>
    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors) AddError(error.ErrorMessage);

        return CustomResponse();
    }

    /// <summary>
    ///     Custom response returns 200 OK if operation is valid, otherwise returns 400 Bad Request
    ///     with custom error messages in the response body
    /// </summary>
    /// <param name="validationResult"> The ValidationResult to be validated </param>
    /// <returns>
    ///     <see cref="ActionResult" />
    /// </returns>
    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors) AddError(error.ErrorMessage);

        return CustomResponse();
    }

    /// <summary>
    ///     Helper method to check if there are any errors in the errors Collection
    /// </summary>
    protected bool IsOperationValid()
    {
        return !_errors.Any();
    }

    protected void AddError(string error)
    {
        _errors.Add(error);
    }

    protected void ClearErrors()
    {
        _errors.Clear();
    }
}