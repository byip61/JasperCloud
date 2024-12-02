using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace JasperCloud.Service;

/// <summary>
/// Service for Blob Storage
/// </summary>
internal sealed class BlobService(BlobServiceClient blobServiceClient, IConfiguration configuration) : IBlobService
{
    private readonly string _containerName = configuration["General:BlobContainerName"]!;

    public async Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

        var fileGuid = Guid.NewGuid();
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileGuid.ToString());

        await blobClient.UploadAsync(
            stream, 
            new BlobHttpHeaders { ContentType = contentType },
            cancellationToken: cancellationToken);
        
        return fileGuid;
    }

    public async Task<FileResponse> DownloadAsync(Guid fileGuid, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileGuid.ToString());

        Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

        return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }

    public async Task DeleteAsync(Guid fileGuid, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileGuid.ToString());

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}