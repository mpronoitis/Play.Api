using Microsoft.AspNetCore.RateLimiting;

namespace Play.Services.Api.Controllers.Core;

[Route("auth/user")]

public class UserController : ApiController
{
    private readonly IJwtBuilder _jwtBuilder;
    private readonly IUserService _service;

    public UserController(IUserService service, IJwtBuilder jwtBuilder)
    {
        _service = service;
        _jwtBuilder = jwtBuilder;
    }

    /// <summary>
    ///     Register a new user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "model")]
    public async Task<IActionResult> Register([FromBody] UserViewModel model)
    {
        if (!ModelState.IsValid) CustomResponse(ModelState);
        var res = await _service.Register(model);

        if (res.IsValid)
        {
            //get user
            var user = await _service.GetUserByEmailAsync(model.Email);
            //generate token
            var token = _jwtBuilder.GenerateToken(user);
            //return token
            return CustomResponse(new { token });
        }

        return CustomResponse(res);
    }

    /// <summary>
    ///     Get All Users from Database with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    [HttpGet("get-all/{page:int}/{pageSize:int}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAll(int page, int pageSize)
    {
        var res = await _service.GetAllAsync(page, pageSize);
        return CustomResponse(res);
    }

    /// <summary>
    ///     Update user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "model")]
    public async Task<IActionResult> Update([FromBody] UpdateUserViewModel model)
    {
        if (!ModelState.IsValid) CustomResponse(ModelState);
        var res = await _service.Update(model);

        if (res.IsValid)
        {
            //get user
            var user = await _service.GetUserByEmailAsync(model.Email);
            //generate token
            var token = _jwtBuilder.GenerateToken(user);
            //return token
            return CustomResponse(new { token });
        }

        return CustomResponse(res);
    }

    /// <summary>
    ///     Update Role
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("update-role")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "model")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleUserViewModel model)
    {
        {
            {
                if (!ModelState.IsValid) CustomResponse(ModelState);
                var res = await _service.UpdateRole(model);

                if (res.IsValid)
                {
                    //get user
                    var user = await _service.GetUserByEmailAsync(model.Email);
                    //generate token
                    var token = _jwtBuilder.GenerateToken(user);
                    //return token
                    return CustomResponse(new { token });
                }

                return CustomResponse(res);
            }
        }
    }

    /// <summary>
    ///     Update Password
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("update-password")]
    [Authorize(Roles = "Customer,PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "model")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordUserViewModel model)
    {
        if (!ModelState.IsValid) CustomResponse(ModelState);
        var res = await _service.UpdatePassword(model);

        if (res.IsValid)
        {
            //get user
            var user = await _service.GetUserByEmailAsync(model.Email);
            //generate token
            var token = _jwtBuilder.GenerateToken(user);
            //return token
            return CustomResponse(new { token });
        }

        return CustomResponse(res);
    }
    
    /// <summary>
    ///     Login user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [RateLimit(PeriodInSec = 60, Limit = 10, BodyParams = "model")]
    public async Task<IActionResult> Login([FromBody] UserViewModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var res = await _service.LoginAsync(model);
        if (!res.Item1)
        {
            AddError("Invalid user or password");
            return CustomResponse();
        }

        var user = await _service.GetUserByEmailAsync(model.Email);

        var token = _jwtBuilder.GenerateToken(user);

        return CustomResponse(new
        {
            token,
            lastLogin = res.Item2,
            failedAttempts = user.FailedLoginAttempts
        });
    }
    /// <summary>
    /// Login WHMCS BOT
    /// </summary>
    ///  /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("loginbot")]
    [EnableRateLimiting("WhmcsLoginBot")]
    public async Task<IActionResult> LoginBot([FromBody] UserViewModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var res = await _service.LoginBotAsync(model);
        if (!res.Item1)
        {
            AddError("Invalid user or password");
            return CustomResponse();
        }

        var user = await _service.GetUserByEmailAsync(model.Email);

        var token = _jwtBuilder.GenerateToken(user);

        return CustomResponse(new
        {
            token,
            lastLogin = res.Item2,
            failedAttempts = user.FailedLoginAttempts
        });
    }

    /// <summary>
    ///     Forgot Password
    /// </summary>
    [HttpPost("forgot-password")]
    [RateLimit(PeriodInSec = 60, Limit = 10, BodyParams = "model")]
    public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var res = await _service.ForgotPassword(model);
        if (!res.IsValid)
        {
            AddError("Invalid user or password");
            return CustomResponse();
        }

        return CustomResponse(res);
    }


    /// <summary>
    ///     Validate token
    /// </summary>
    /// <returns></returns>
    [HttpGet("validate-token")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public IActionResult ValidateToken()
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var res = _jwtBuilder.DecodeToken(token);
            return CustomResponse(new { valid = true });
        }
        catch (IntegrityException e)
        {
            return CustomResponse(new { valid = false, message = e.Message });
        }
        catch (Exception e)
        {
            return CustomResponse(new { valid = false, message = e.Message });
        }
    }

    /// <summary>
    ///     Refresh token
    /// </summary>
    [HttpGet("refresh-token")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public IActionResult RefreshToken()
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var res = _jwtBuilder.RefreshToken(token);
            return CustomResponse(new { token = res });
        }
        catch (IntegrityException e)
        {
            return CustomResponse(new { valid = false, message = e.Message });
        }
        catch (Exception e)
        {
            return CustomResponse(new { valid = false, message = e.Message });
        }
    }

    /// <summary>
    ///     Get total count of users with a given role
    ///     Optionally pass a time range to get count of users with a given role with CreatedAt in that range
    /// </summary>
    /// <param name="role"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    [HttpGet("count-by-role/{role}/{from?}/{to?}")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> GetCountByRole(string role, DateTime? from = null, DateTime? to = null)
    {
        var res = await _service.GetTotalCountByRoleAsync(role, from, to);
        return CustomResponse(new { count = res });
    }


    /// <summary>
    ///     Remove user by id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("remove/{id}")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> Remove(Guid id)
    {
        if (!ModelState.IsValid) CustomResponse(ModelState);
        var res = await _service.Remove(id);
        return CustomResponse(res);
    }

    /// <summary>
    ///     Get total count of users
    /// </summary>
    /// <returns></returns>
    [HttpGet("count")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> Count()
    {
        var res = await _service.GetTotalCountAsync();
        return CustomResponse(res);
    }

    /// <summary>
    ///     Get count of users created within a given time range
    /// </summary>
    /// <param name="startDateTime"></param>
    /// <param name="endDateTime"></param>
    /// <returns></returns>
    [HttpGet("count-by-date/{startDateTime}/{endDateTime}")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> CountByDate(DateTime startDateTime, DateTime endDateTime)
    {
        var res = await _service.GetCountByCreatedAtAsync(startDateTime, endDateTime);
        return CustomResponse(res);
    }
}