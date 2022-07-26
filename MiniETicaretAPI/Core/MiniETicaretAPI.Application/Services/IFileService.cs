using Microsoft.AspNetCore.Http;

namespace MiniETicaretAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);

        Task<string> FileRenameAsync(string fileNamme);
        Task<bool> CopyFileAsync(string path, IFormFile file);
    }
}
