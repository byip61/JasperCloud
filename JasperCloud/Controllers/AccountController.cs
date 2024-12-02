using JasperCloud.RequestModels;
using JasperCloud.Service;
using Microsoft.AspNetCore.Mvc;

namespace JasperCloud.Controllers;

[Route("api/account/")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountController(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
    {
        _accountService = accountService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Route("/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("createaccount")]
    public async Task<IActionResult> CreateAccount(UserRequest userRequest)
    {
        await _accountService.CreateAccountAsync(userRequest);

        return StatusCode(200);
    }

    [HttpGet]
    [Route("login")]
    public async Task<bool> UserLogin(LoginRequest loginRequest)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var user = await _accountService.UserLoginAsync(loginRequest);

        if (user == null) return false;
        else
        {
            session.SetInt32("userid", user.Id);
            session.SetString("username", user.Username!);
            session.SetString("email", user.Email!);
        }

        return true;
    }

    [HttpPost]
    [Route("changepassword")]
    public async Task<IActionResult> ChangePassword(string password)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var userId = session.GetInt32("userid");

        var isPassChanged = await _accountService.ChangePasswordAsync(userId!.Value, password);

        if (isPassChanged) return StatusCode(200);
        else return StatusCode(400);
    }
}