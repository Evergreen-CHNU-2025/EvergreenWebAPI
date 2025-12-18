using EvergreenWebAPI.DTOs;

namespace EvergreenWebAPI.Services.Abstractions;

public interface IFlowerMlClient
{
    Task<IReadOnlyList<FlowerPredictionDTO>> PredictAsync(
        Stream imageStream,
        string fileName,
        string contentType = "image/jpeg",
        CancellationToken ct = default);
}