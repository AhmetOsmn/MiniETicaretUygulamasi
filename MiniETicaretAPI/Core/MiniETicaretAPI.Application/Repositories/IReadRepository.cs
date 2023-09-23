using MiniETicaretAPI.Domain.Entities.Common;
using System.Linq.Expressions;

namespace MiniETicaretAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        // IQueryable ile calistigimiz zaman verilere sorgular ile erismis oluruz.
        // IEnumerable ile calisirsak verilen once inmemory'e cekilir ve oradan erisim saglamis oluruz.

        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
