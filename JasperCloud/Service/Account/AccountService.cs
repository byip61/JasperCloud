using JasperCloud.Algorithms;
using JasperCloud.Models;
using JasperCloud.Repository;
using JasperCloud.RequestModels;
using JasperCloud.ResponseModels;

namespace JasperCloud.Service;

/// <summary>
/// Service for Login
/// </summary>
public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepo;

    public AccountService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task CreateAccountAsync(UserRequest userResult)
    {
        var (hash, salt) = Password.Hash(userResult.Password!);
        User user = new User {
            Username = userResult.Username!,
            Email = userResult.Email!,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _userRepo.AddAsync(user);
    }

    public async Task<LoginResponse?> UserLoginAsync(LoginRequest loginResult)
    {
        try 
        {
            var user = await _userRepo.GetByUsernameAsync(loginResult.Username!);

            if (user == null) return null;

            var isCorrectPass = Password.CheckMatch(loginResult.Password!, user.PasswordHash, user.PasswordSalt);

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
}