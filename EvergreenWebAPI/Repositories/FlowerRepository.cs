using EvergreenWebAPI.Models;
using EvergreenWebAPI.Repositories.Abstractions;

namespace EvergreenWebAPI.Repositories;

public class FlowerRepository : GenericRepository<Flower>, IFlowerRepository
{
    public FlowerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}