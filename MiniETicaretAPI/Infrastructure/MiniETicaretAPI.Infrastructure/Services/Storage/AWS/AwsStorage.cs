using Microsoft.AspNetCore.Http;
using MiniETicaretAPI.Application.Abstactions.Storage.AWS;

namespace MiniETicaretAPI.Infrastructure.Services.Storage.AWS
{
    public class AwsStorage : IAwsStorage
    {
        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            throw new NotImplementedException();
        }
    }
}
