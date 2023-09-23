using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Persistence.Contexts;

namespace MiniETicaretAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(MiniETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
