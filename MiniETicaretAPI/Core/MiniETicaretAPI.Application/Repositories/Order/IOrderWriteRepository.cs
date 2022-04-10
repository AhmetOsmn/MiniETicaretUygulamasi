using MiniETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaretAPI.Application.Repositories
{
    public interface IOrderWriteRepository : IWriteRepository<Order>
    {
    }
}
