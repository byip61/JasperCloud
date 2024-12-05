using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JasperCloud.ViewModels;
using Microsoft.AspNetCore.Authorization;
using JasperCloud.Service;
using System.Security.Claims;

namespace JasperCloud.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFileService _fileService;

    public HomeController(ILogger<HomeController> logger, IFileService fileService)
    {
        _logger = logger;
        _fileService = fileService;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = HttpContext.User;
        var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Convert.ToInt32(userIdStr);

        var files = await _fileService.GetAllFilesByUserIdAsync(userId);

        return View(files);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

