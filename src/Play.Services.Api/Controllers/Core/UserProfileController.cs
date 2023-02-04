namespace Play.Services.Api.Controllers.Core;

[Route("auth/user-profile")]
public class UserProfileController : ApiController
{
    private readonly IJwtBuilder _jwtBuilder;
    private readonly IUserProfileService _service;

    public UserProfileController(IUserProfileService service, IJwtBuilder jwtBuilder)
    {
        _service = service;
        _jwtBuilder = jwtBuilder;
    }


    /// <summary>
    ///     Update profile user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorize(Roles = "Customer,PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "model")]
    public async Task<IActionResult> Update([FromBody] UserProfileViewModel model)
    {
        return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _service.Update(model));
    }

    // <summary>
    /// Get profile user id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (!ModelState.IsValid) CustomResponse(ModelState);
        var res = await _service.GetAsync(id);

        return CustomResponse(res);
    }

    /// <summary>
    ///     Get all user profiles
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("all/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<UserProfileViewModel>> GetAll(int page, int pageSize)
    {
        return await _service.GetAll(page, pageSize);
    }

    // <summary>
    /// Get profile user id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("byUser/{userid:guid}")]
    [Authorize(Roles = "Customer,PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "userid")]
    public async Task<IActionResult> GetByUserId(Guid userid)
    {
        if (!ModelState.IsValid) CustomResponse(ModelState);
        var res = await _service.GetByUserId(userid);

        return CustomResponse(res);
    }
}