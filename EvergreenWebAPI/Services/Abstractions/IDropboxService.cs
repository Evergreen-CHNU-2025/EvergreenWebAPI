using EvergreenWebAPI.Models;

namespace EvergreenWebAPI.Services.Abstractions;

public interface IDropboxService
{
    Task UploadFileAsync(string fileName, IFormFile file, string folder);
    Task<string> GetTemporaryLinkAsync(string folder, string fileName);
    Task<bool> DeleteFileAsync(string filePath);
}