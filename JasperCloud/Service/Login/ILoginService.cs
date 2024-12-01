using JasperCloud.RequestModels;
using JasperCloud.ResponseModels;

namespace JasperCloud.Service;

public interface ILoginService
{
    Task CreateAccount(UserRequest userResult);
    
    Task<LoginResponse> UserLogin(LoginRequest loginResult);
}