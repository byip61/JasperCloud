using JasperCloud.ViewModels;
using JasperCloud.ResponseModels;

namespace JasperCloud.Service;

/// <summary>
/// Interface for AccountService
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Creates an account and adds it to the database.
    /// </summary>
    /// <param name="newUser"></param>
    Task CreateAccountAsync(CreateAccountViewModel newUser);
    
    /// <summary>
    /// Logs in.
    /// </summary>
    /// <param name="loginViewModel"></param>
    /// <returns></returns>
    Task<LoginResponse?> UserLoginAsync(LoginViewModel loginViewModel);

    /// <summary>
    /// Change user's password.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns>True if successful, false if failed.</returns>
    Task<bool> ChangePasswordAsync(int userId, string password);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <returns>True if exists, false if not.</returns>
    Task<bool> CheckUserExistsAsync(string username, string email);
}