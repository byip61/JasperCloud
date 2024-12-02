using JasperCloud.Models;

namespace JasperCloud.Repository;

/// <summary>
/// Interface for UserRepository
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Get one user by user ID.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A nullable class object of User.</returns>
    Task<User?> GetByUserIdAsync(int userId);

    /// <summary>
    /// Get one user by username.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>A nullable class object of User.</returns>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// Inserts a new user.
    /// </summary>
    /// <param name="user"></param>
    Task AddAsync(User user);

    /// <summary>
    /// Updates a user's password.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="hash">Password hash.</param>
    /// <param name="salt"></param>
    /// <returns>True if successful, false if failed.</returns>
    Task<bool> UpdatePasswordAsync(int userId, string hash, string salt);
}