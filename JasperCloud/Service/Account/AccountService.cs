using JasperCloud.Algorithms;
using JasperCloud.Models;
using JasperCloud.Repository;
using JasperCloud.ViewModels;
using JasperCloud.ResponseModels;

namespace JasperCloud.Service;

/// <summary>
/// Service for Account
/// </summary>
public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepo;

    public AccountService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task CreateAccountAsync(CreateAccountViewModel newUser)
    {
        var (hash, salt) = Password.Hash(newUser.Password!);
        User user = new User {
            Username = newUser.Username!,
            Email = newUser.Email!,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _userRepo.AddAsync(user);
    }

    public async Task<LoginResponse?> UserLoginAsync(LoginViewModel loginViewModel)
    {
        try 
        {
            var user = await _userRepo.GetByUsernameAsync(loginViewModel.Username!);

            if (user == null) return null;

            var isCorrectPass = Password.CheckMatch(loginViewModel.Password!, user.PasswordHash, user.PasswordSalt);

            if (!isCorrectPass) return null;

            var loginResponse = new LoginResponse {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return loginResponse;
        }
        catch (HttpRequestException error)
        {
            throw new HttpRequestException(error.Message);
        }
    }

    public async Task<bool> ChangePasswordAsync(int userId, string password)
    {
        var (hash, salt) = Password.Hash(password);

        return await _userRepo.UpdatePasswordAsync(userId, hash, salt);
    }

    public async Task<bool> CheckUserExistsAsync(string username, string email)
    {
        var user = await _userRepo.GetByUsernameOrEmailAsync(username, email);

        if (user == null) return false;
        else return true;
    }
}