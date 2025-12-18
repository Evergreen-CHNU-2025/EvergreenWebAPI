using System.Text.Json.Serialization;

namespace EvergreenWebAPI.DTOs;

public class FlowerPredictionDTO
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("image_path")]
    public string ImageUrl { get; set; } = default!;

    [JsonPropertyName("probability")]
    public float Probability { get; set; }
}