using JasperCloud.Algorithms;
using JasperCloud.Models;
using JasperCloud.Repository;
using JasperCloud.RequestModels;
using JasperCloud.ResponseModels;

namespace JasperCloud.Service;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepo;

    public LoginService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task CreateAccount(UserRequest userResult)
    {
        var (hash, salt) = Password.Hash(userResult.Password);
        User user = new User {
            Username = userResult.Username!,
            Email = userResult.Email!,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _userRepo.AddAsync(user);
    }

    public async Task<LoginResponse> UserLogin(LoginRequest loginResult)
    {
        try 
        {
            var user = await _userRepo.GetByUsernameAsync(loginResult.Username!);

            var isCorrectPass = Password.CheckMatch(loginResult.Password!, user.PasswordHash, user.PasswordSalt);

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
}