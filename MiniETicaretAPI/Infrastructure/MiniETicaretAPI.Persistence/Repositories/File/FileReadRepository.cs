using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Persistence.Contexts;

namespace MiniETicaretAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(MiniETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
