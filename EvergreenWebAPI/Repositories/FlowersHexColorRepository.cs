using EvergreenWebAPI.Models;
using EvergreenWebAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EvergreenWebAPI.Repositories;

public class FlowersHexColorRepository : GenericRepository<FlowersHexColor>, IFlowersHexColorRepository
{
    public FlowersHexColorRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<string[]> GetFlowerColors(Guid flowerId)
        => [.. dbContext.FlowersHexColors
            .Include(fhc => fhc.Color)
            .Where(fhc => fhc.FlowerId == flowerId)
            .Select(fhc => fhc.Color.Color)];
}