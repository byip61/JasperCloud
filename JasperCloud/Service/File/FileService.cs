using JasperCloud.Repository;

namespace JasperCloud.Service;

/// <summary>
/// Service for File
/// </summary>
public class FileService : IFileService
{
    private readonly IFileRepository _fileRepo;
    private readonly IUserRepository _userRepo;

    public FileService(IFileRepository fileRepo, IUserRepository userRepo)
    {
        _fileRepo = fileRepo;
        _userRepo = userRepo;
    }

    public async Task<Guid> UploadFileAsync(int userId, IFormFile file, IBlobService blobService)
    {
        Stream stream = file.OpenReadStream();

        var fileGuid = await blobService.UploadAsync(stream, file.ContentType);
        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var size = file.Length;
        var uploadDate = DateTime.Now;

        var user = await _userRepo.GetByUserIdAsync(userId);

        var fileObj = new Models.File {
            UserId = userId,
            //User = user!,
            FileGuid = fileGuid,
            Name = fileName,
            Extension = extension,
            Size = size,
            UploadDate = uploadDate
        };

        await _fileRepo.AddFileAsync(fileObj);

        return fileGuid;
    }

    public async Task<(FileResponse?, Models.File?)> DownloadFileAsync(int userId, Guid fileGuid, IBlobService blobService)
    {
        var file = await _fileRepo.GetFileByFileGuidAsync(fileGuid);
        var fileUserId = file.UserId;

        if (userId != Convert.ToInt32(fileUserId)) return (null, null);

        var fileResponse = await blobService.DownloadAsync(fileGuid);

        if (fileResponse == null) return (null, null);

        return (fileResponse, file);
    }

    public async Task<bool> DeleteFileAsync(int userId, Guid fileGuid, IBlobService blobService)
    {
        var file = await _fileRepo.GetFileByFileGuidAsync(fileGuid);
        var fileUserId = file.UserId;

        if (userId != Convert.ToInt32(fileUserId)) return false;

        await _fileRepo.DeleteFileAsync(fileGuid);
        await blobService.DeleteAsync(fileGuid);

        return true;
    }

    public async Task<List<Models.File>?> GetAllFilesByUserIdAsync(int userId)
        => await _fileRepo.GetAllByUserId(userId);
}