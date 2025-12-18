using EvergreenWebAPI.DTOs;
using EvergreenWebAPI.Repositories.Abstractions;
using EvergreenWebAPI.Services.Abstractions;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EvergreenWebAPI.Services;

public class FlowerService : IFlowerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public FlowerService(IUnitOfWork unitOfWork, HttpClient http)
    {
        _unitOfWork = unitOfWork;
        _http = http;
    }

    public async Task<FlowerInfoDTO?> GetFlowerInfoByIdAsync(Guid flowerId)
    {
        var flower = await _unitOfWork.FlowerRepository.GetByIdAsync(flowerId);

        if (flower == null)
            return null;

        return new FlowerInfoDTO
        {
            Id = flower.Id,
            NameUa = flower.NameUa,
            NameLat = flower.NameLat,
            Description = flower.Description,
            ImagePath = flower.ImagePath,
            Symbolics = flower.Symbolics,
            Meaning = flower.Meaning,
            InspectRecomendations = flower.InspectRecomendations,
            Colors = await _unitOfWork.FlowersHexColorRepository.GetFlowerColors(flowerId)
        };
    }
}