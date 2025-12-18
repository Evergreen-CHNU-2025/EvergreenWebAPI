using EvergreenWebAPI.Models;

namespace EvergreenWebAPI.Repositories.Abstractions;

public interface IFlowersHexColorRepository : IGenericRepository<FlowersHexColor>
{
    Task<string[]> GetFlowerColors(Guid flowerId);
}