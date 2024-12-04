using JasperCloud.Data;
using Microsoft.EntityFrameworkCore;

namespace JasperCloud.Repository;

/// <summary>
/// Repository for File
/// </summary>
public class FileRepository : IFileRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FileRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> GetUserIdByFileGuidAsync(Guid fileGuid)
    {
        return await _dbContext.Files
            .Where(f => f.FileGuid == fileGuid)
            .Select(f => f.UserId)
            .FirstOrDefaultAsync();
    }

    public async Task<Models.File> GetFileByFileGuidAsync(Guid fileGuid)
    {
        try
        {
            var file = await _dbContext.Files.FirstOrDefaultAsync(f => f.FileGuid == fileGuid);

            return file!;
        }
        catch (NullReferenceException error)
        {
            throw new NullReferenceException(error.Message);
        }
    }

    public async Task AddFileAsync(Models.File file)
    {
        try
        {
            await _dbContext.Files.AddAsync(file);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException error)
        {
            throw new DbUpdateException(error.Message);
        }
    }

    public async Task DeleteFileAsync(Guid fileGuid)
    {
        try
        {
            var file = await GetFileByFileGuidAsync(fileGuid);

            _dbContext.Files.Attach(file);
            _dbContext.Files.Remove(file);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException error)
        {
            throw new DbUpdateException(error.Message);
        }
    }
}