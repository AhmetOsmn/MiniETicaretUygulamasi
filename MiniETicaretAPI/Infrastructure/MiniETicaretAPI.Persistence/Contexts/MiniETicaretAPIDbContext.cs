using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Domain.Entities;
using MiniETicaretAPI.Domain.Entities.Common;
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

        // repository'de kullanilan savechangesasync metodunu override ediyoruz.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker: Entity'ler uzerinden yapilan degisikliklerin ya da yeni eklenen verinin yakalanmasini sağlayan prop'tur. 
            // Update operasyonlarinda Track edilen verileri yakalayip elde etmemizi saglar.

            var datas = ChangeTracker.Entries<BaseEntity>();    // surece giren butun baseentity'leri yakalayacak.

            foreach (var item in datas)
            {
                _ = item.State switch
                {
                    EntityState.Added => item.Entity.CreatedDate = DateTime.UtcNow, // nesne ilk defa ekleniyor ise createddate' e atama yap.
                    EntityState.Modified => item.Entity.UpdatedDate = DateTime.UtcNow,  // var olan nesne guncelleniyor ise updateddate' e atama yap.
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
