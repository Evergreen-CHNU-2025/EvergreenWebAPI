using EvergreenWebAPI.Models;
using EvergreenWebAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EvergreenWebAPI.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsEmailExist(string email)
        => await dbContext.Users.AnyAsync(u => u.Email.Equals(email));

    public async Task<User?> FindByEmailAsync(string email)
        => await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
}