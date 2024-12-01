using JasperCloud.RequestModels;
using JasperCloud.Service;
using Microsoft.AspNetCore.Mvc;

namespace JasperCloud.Controllers;

public class LoginController : Controller
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("createaccount")]
    public async Task<IActionResult> CreateAccount(UserRequest userRequest)
    {
        await _loginService.CreateAccount(userRequest);

        return StatusCode(200);
    }

    [HttpGet]
    [Route("userlogin")]
    public async Task<IActionResult> UserLogin(LoginRequest loginRequest)
    {
        var user = await _loginService.UserLogin(loginRequest);

        HttpContext.Session.SetInt32("id", user.Id);
        HttpContext.Session.SetString("username", user.Username!);
        HttpContext.Session.SetString("email", user.Email!);

        return StatusCode(200);
    }
}