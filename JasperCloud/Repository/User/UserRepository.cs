using JasperCloud.Data;
using JasperCloud.Models;
using Microsoft.EntityFrameworkCore;

namespace JasperCloud.Repository;

/// <summary>
/// Repository for User
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByUserIdAsync(int userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdatePasswordAsync(int userId, string hash, string salt)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return false;
        
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}