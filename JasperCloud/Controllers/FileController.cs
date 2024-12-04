using System.Security.Claims;
using JasperCloud.Service;
using Microsoft.AspNetCore.Mvc;

namespace JasperCloud.Controllers;

[Route("[controller]/[action]")]
public class FileController : Controller
{
    private readonly IFileService _fileService;
    private readonly IBlobService _blobService;

    public FileController(IFileService fileService, IBlobService blobService)
    {
        _fileService = fileService;
        _blobService = blobService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var user = HttpContext.User;
        var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Convert.ToInt32(userIdStr);
        
        await _fileService.UploadFileAsync(userId, file, _blobService);

        return Redirect("/Home/Index");
    }

    [HttpGet]
    public async Task<IResult> Download(Guid fileGuid)
    {
        var user = HttpContext.User;
        var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Convert.ToInt32(userIdStr);
        
        var (fileResponse, file) = await _fileService.DownloadFileAsync(userId, fileGuid, _blobService);

        if (fileResponse == null || file == null) throw new InvalidOperationException("Failed to download file.");

        var fileDownloadName = file!.Name + file!.Extension;

        return Results.File(fileResponse!.Stream, fileResponse.ContentType, fileDownloadName);
    }

    [HttpDelete]
    public async Task Delete(Guid fileGuid)
    {
        var user = HttpContext.User;
        var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Convert.ToInt32(userIdStr);
        
        await _fileService.DeleteFileAsync(userId, fileGuid, _blobService);
    }
}