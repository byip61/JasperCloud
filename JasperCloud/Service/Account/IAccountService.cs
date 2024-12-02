using JasperCloud.RequestModels;
using JasperCloud.ResponseModels;

namespace JasperCloud.Service;

/// <summary>
/// Interface for LoginService
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Creates an account and adds it to the database.
    /// </summary>
    /// <param name="userResult"></param>
    Task CreateAccountAsync(UserRequest userResult);
    
    /// <summary>
    /// Logs in.
    /// </summary>
    /// <param name="loginResult"></param>
    /// <returns></returns>
    Task<LoginResponse?> UserLoginAsync(LoginRequest loginResult);

    /// <summary>
    /// Change user's password.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns>True if successful, false if failed.</returns>
    Task<bool> ChangePasswordAsync(int userId, string password);
}