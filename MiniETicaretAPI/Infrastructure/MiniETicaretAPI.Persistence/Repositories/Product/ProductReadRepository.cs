using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Domain.Entities;
using MiniETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaretAPI.Persistence.Repositories
{
    internal class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(MiniETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
