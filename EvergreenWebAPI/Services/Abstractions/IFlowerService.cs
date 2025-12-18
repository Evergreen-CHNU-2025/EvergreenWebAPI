using EvergreenWebAPI.DTOs;

namespace EvergreenWebAPI.Services.Abstractions;

public interface IFlowerService
{
    Task<FlowerInfoDTO?> GetFlowerInfoByIdAsync(Guid flowerId);
}