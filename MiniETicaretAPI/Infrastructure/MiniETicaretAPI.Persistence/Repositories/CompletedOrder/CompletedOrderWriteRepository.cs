using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Domain.Entities;
using MiniETicaretAPI.Persistence.Contexts;

namespace MiniETicaretAPI.Persistence.Repositories
{
    public class CompletedOrderWriteRepository : WriteRepository<CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(MiniETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
