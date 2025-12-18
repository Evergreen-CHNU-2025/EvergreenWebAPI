using Dropbox.Api;
using Dropbox.Api.Files;
using EvergreenWebAPI.Models;
using EvergreenWebAPI.Services.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace EvergreenWebAPI.Services;

public class DropboxService : IDropboxService
{
    private const string DropboxRequestLink = "https://api.dropboxapi.com/2/files/get_temporary_link";

    private readonly IConfiguration _configuration;
    private readonly string _accessToken;
    private HttpClient _httpClient;

    public DropboxService(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _accessToken = _configuration["DropboxAPI:Token"]!;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task UploadFileAsync(string fileName, IFormFile file, string folder)
    {
        using (var dbxClient = new DropboxClient(_accessToken))
        {
            byte[] fileBytes;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            using (var memoryStream = new MemoryStream(fileBytes))
            {
                await dbxClient.Files.UploadAsync($"/{folder}/{fileName}",
                    WriteMode.Overwrite.Instance,
                    body: memoryStream);
            }
        }
    }

    public async Task<string> GetTemporaryLinkAsync(string folder, string fileName)
    {
        var requestBody = new { path = $"/{folder}/{fileName}" };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(DropboxRequestLink, content);

        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();
        var jsonResponse = JObject.Parse(responseContent);

        return jsonResponse["link"]?.ToString() ?? string.Empty;
    }

    public async Task<bool> DeleteFileAsync(string filePath)
    {
        bool isDeleted = false;

        using (var dbxClient = new DropboxClient(_accessToken))
        {
            var responce = await dbxClient.Files.DeleteV2Async(filePath);
            isDeleted = responce.Metadata.IsDeleted;
        }

        return isDeleted;
    }

}