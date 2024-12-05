namespace JasperCloud.Service;

/// <summary>
/// Interface for FileService
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Uploads file for user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="file"></param>
    /// <param name="blobService"></param>
    /// <returns>File GUID.</returns>
    Task<Guid> UploadFileAsync(int userId, IFormFile file, IBlobService blobService);

    /// <summary>
    /// Downloads file from server.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="fileGuid"></param>
    /// <param name="blobService"></param>
    /// <returns>FileResponse record.</returns>
    Task<(FileResponse?, Models.File?)> DownloadFileAsync(int userId, Guid fileGuid, IBlobService blobService);

    /// <summary>
    /// Deletes file from server.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="fileGuid"></param>
    /// <param name="blobService"></param>
    /// <returns>True if succeeded; false if failed.</returns>
    Task<bool> DeleteFileAsync(int userId, Guid fileGuid, IBlobService blobService);

    /// <summary>
    /// Gets all files by user ID.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>List of files or null.</returns>
    Task<List<Models.File>?> GetAllFilesByUserIdAsync(int userId);
}