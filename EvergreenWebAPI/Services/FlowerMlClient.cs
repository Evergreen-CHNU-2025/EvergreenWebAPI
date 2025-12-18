using EvergreenWebAPI.DTOs;
using EvergreenWebAPI.Services.Abstractions;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EvergreenWebAPI.Services;

public class FlowerMlClient : IFlowerMlClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public FlowerMlClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<FlowerPredictionDTO>> PredictAsync(
        Stream imageStream,
        string fileName,
        string contentType = "image/jpeg",
        CancellationToken ct = default)
    {
        using var form = new MultipartFormDataContent();

        var fileContent = new StreamContent(imageStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        form.Add(fileContent, "file", fileName);

        using var resp = await _http.PostAsync("/predict", form, ct);

        var body = await resp.Content.ReadAsStringAsync(ct);

        if (!resp.IsSuccessStatusCode)
            throw new HttpRequestException($"Flask /predict failed ({(int)resp.StatusCode}): {body}");

        var data = JsonSerializer.Deserialize<List<FlowerPredictionDTO>>(body, JsonOpts)
                   ?? new List<FlowerPredictionDTO>();

        return data;
    }
}