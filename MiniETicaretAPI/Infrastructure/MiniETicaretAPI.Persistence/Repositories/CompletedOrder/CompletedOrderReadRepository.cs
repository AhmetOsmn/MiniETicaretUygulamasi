using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Domain.Entities;
using MiniETicaretAPI.Persistence.Contexts;

namespace MiniETicaretAPI.Persistence.Repositories
{
    public class CompletedOrderReadRepository : ReadRepository<CompletedOrder>, ICompletedOrderReadRepository
    {
        public CompletedOrderReadRepository(MiniETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
