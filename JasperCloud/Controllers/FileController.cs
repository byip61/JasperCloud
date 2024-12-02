using JasperCloud.RequestModels;
using JasperCloud.Service;
using Microsoft.AspNetCore.Mvc;

namespace JasperCloud.Controllers;

[Route("api/file/")]
public class FileController : Controller
{
    private readonly IFileService _fileService;
    private readonly IBlobService _blobService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileController(IFileService fileService, IBlobService blobService, IHttpContextAccessor httpContextAccessor)
    {
        _fileService = fileService;
        _blobService = blobService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var userId = session.GetInt32("userid");
        
        await _fileService.UploadFileAsync(userId!.Value, file, _blobService);

        return StatusCode(200);
    }
}