using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MiniETicaretAPI.Application.Abstactions.Storage.Azure;

namespace MiniETicaretAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(Environment.GetEnvironmentVariable(configuration["Storage:Azure"]));
        }
        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = GetBlobContainerClientWithContainerName(containerName);
            BlobClient blobClient =  _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = GetBlobContainerClientWithContainerName(containerName);
            return _blobContainerClient.GetBlobs().Select(blob => blob.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = GetBlobContainerClientWithContainerName(containerName);
            return _blobContainerClient.GetBlobs().Any(blob => blob.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = GetBlobContainerClientWithContainerName(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
                string newFileName = await FileRenameAsync(containerName, file.FileName, HasFile);

                BlobClient blobClient = _blobContainerClient.GetBlobClient(newFileName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((newFileName, containerName));
            }

            return datas;
        }

        private BlobContainerClient GetBlobContainerClientWithContainerName(string containerName)
        {
            return _blobServiceClient.GetBlobContainerClient(containerName);
        }
    }
}
