namespace JasperCloud.Repository;

/// <summary>
/// Interface for FileRepository
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// Gets the user ID by file GUID.
    /// </summary>
    /// <param name="fileGuid"></param>
    /// <returns>FileUserIdResponse object or null.</returns>
    Task<int?> GetUserIdByFileGuidAsync(Guid fileGuid);

    /// <summary>
    /// Gets the file by file GUID.
    /// </summary>
    /// <param name="fileGuid"></param>
    /// <returns>File object.</returns>
    Task<Models.File> GetFileByFileGuidAsync(Guid fileGuid);

    /// <summary>
    /// Adds file to database.
    /// </summary>
    /// <param name="file"></param>
    Task AddFileAsync(Models.File file);

    /// <summary>
    /// Deletes file from database.
    /// </summary>
    /// <param name="fileGuid"></param>
    Task DeleteFileAsync(Guid fileGuid);

    /// <summary>
    /// Gets all files by user ID.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>List of all files from user or null.</returns>
    Task<List<Models.File>?> GetAllByUserId(int userId);
}