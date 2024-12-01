using JasperCloud.Models;

namespace JasperCloud.Repository;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);

    Task AddAsync(User user);
}