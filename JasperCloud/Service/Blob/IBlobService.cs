namespace JasperCloud.Service;

/// <summary>
/// Interface for BlobService
/// </summary>
public interface IBlobService
{
    /// <summary>
    /// Uploads a file to blob storage.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="contentType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The GUID of the file in the blob storage.</returns>
    Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads a file from the blob storage.
    /// </summary>
    /// <param name="fileGuid">The GUID of the file.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>FileResponse record.</returns>
    Task<FileResponse> DownloadAsync(Guid fileGuid, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a file from the blob storage.
    /// </summary>
    /// <param name="fileGuid">The GUID of the file.</param>
    /// <param name="cancellationToken"></param>
    Task DeleteAsync(Guid fileGuid, CancellationToken cancellationToken = default);
}

public record FileResponse(Stream Stream, string ContentType);