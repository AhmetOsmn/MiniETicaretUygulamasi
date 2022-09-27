using Microsoft.AspNetCore.Http;

namespace MiniETicaretAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);

        Task<bool> CopyFileAsync(string path, IFormFile file);
    }
}
