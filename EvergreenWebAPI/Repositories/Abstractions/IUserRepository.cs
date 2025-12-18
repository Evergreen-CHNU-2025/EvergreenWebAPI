using EvergreenWebAPI.Models;

namespace EvergreenWebAPI.Repositories.Abstractions;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> IsEmailExist(string email);
    Task<User?> FindByEmailAsync(string email);
}