using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaretAPI.Persistence.Contexts
{
    public class MiniETicaretAPIDbContext : DbContext
    {
        public MiniETicaretAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
