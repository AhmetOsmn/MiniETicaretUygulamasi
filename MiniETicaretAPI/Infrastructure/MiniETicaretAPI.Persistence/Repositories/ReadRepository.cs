using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Domain.Entities.Common;
using MiniETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly MiniETicaretAPIDbContext _context;

        public ReadRepository(MiniETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method) => Table.Where(method);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method) => await Table.FirstOrDefaultAsync(method);

        public async Task<T> GetByIdAsync(string id) => await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }
}
