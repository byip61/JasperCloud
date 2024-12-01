using JasperCloud.Algorithms;
using JasperCloud.Data;
using JasperCloud.Models;
using JasperCloud.ResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JasperCloud.Controllers;

public class LoginController : Controller
{
    private readonly ApplicationDbContext _db;

    public LoginController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("createaccount")]
    public async Task CreateAccount(UserResult userResult)
    {
        var (hash, salt) = Password.Hash(userResult.Password);
        User user = new User {
            Username = userResult.Username!,
            Email = userResult.Email!,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    [HttpGet]
    [Route("userlogin")]
    public async Task<IActionResult> UserLogin(LoginResult loginResult)
    {
        try 
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == loginResult.Username);

            if (user == null) return BadRequest();

            var isCorrectPass = Password.CheckMatch(loginResult.Password!, user.PasswordHash, user.PasswordSalt);

            if (!isCorrectPass) return BadRequest();

            HttpContext.Session.SetInt32("id", user.Id);
            HttpContext.Session.SetString("username", user.Username);
            HttpContext.Session.SetString("email", user.Email);

            return Ok();
        }
        catch (HttpRequestException error)
        {
            throw new HttpRequestException(error.Message);
        }
    }
}