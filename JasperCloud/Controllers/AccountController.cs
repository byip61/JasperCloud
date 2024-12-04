using JasperCloud.ViewModels;
using JasperCloud.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace JasperCloud.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult CreateAccount()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Account()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountViewModel newUser)
    {
        var isUserExist = await _accountService.CheckUserExistsAsync(newUser.Username!, newUser.Email!);

        if (isUserExist)
        {
            TempData["ErrorMessage"] = "An account with this email already exists.";

            return Redirect("/Account/CreateAccount");
        }
        else
        {
            await _accountService.CreateAccountAsync(newUser);
            TempData["AccountCreated"] = "Account created! You may login now.";

            return Redirect("/Account/Login");
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> UserLogin(LoginViewModel loginViewModel)
    {
        var user = await _accountService.UserLoginAsync(loginViewModel);

        if (user == null) return Redirect("/Account/Login");
        else
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                claimsPrincipal);
        }

        return Redirect("/Home/Index");
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(string password)
    {
        var user = HttpContext.User;
        var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Convert.ToInt32(userIdStr);

        var isPassChanged = await _accountService.ChangePasswordAsync(userId, password);

        if (isPassChanged) return await UserLogout();
        else return Redirect("/Account/Account");
    }

    [HttpGet]
    public async Task<IActionResult> UserLogout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect("/Account/Login");
    }
}