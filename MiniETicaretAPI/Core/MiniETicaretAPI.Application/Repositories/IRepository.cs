using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Domain.Entities.Common;

namespace MiniETicaretAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
