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

    public async Task<FileResponse?> DownloadFileAsync(int userId, Guid fileGuid, IBlobService blobService)
    {
        var fileUserId = _fileRepo.GetUserIdByFileGuidAsync(fileGuid);

        if (fileUserId == null) return null;
        else if (userId != Convert.ToInt32(fileUserId)) return null;

        return await blobService.DownloadAsync(fileGuid);
    }

    public async Task<bool> DeleteFileAsync(int userId, Guid fileGuid, IBlobService blobService)
    {
        var fileUserId = _fileRepo.GetUserIdByFileGuidAsync(fileGuid);

        if (fileUserId == null) return false;
        else if (userId != Convert.ToInt32(fileUserId)) return false;

        await blobService.DeleteAsync(fileGuid);

        return true;
    }
}