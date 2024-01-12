using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Domain.Entities;
using MiniETicaretAPI.Domain.Entities.Common;
using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Persistence.Contexts
{
    public class MiniETicaretAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public MiniETicaretAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }

        #region Table Per Hierarcy Yaklaşımı
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(o => o.Id);

            builder.Entity<Order>().HasIndex(o => o.OrderCode).IsUnique();

            builder.Entity<Basket>().HasOne(b => b.Order).WithOne(o => o.Basket).HasForeignKey<Order>(b => b.Id);

            builder.Entity<Order>().HasOne(o => o.CompletedOrder).WithOne(co => co.Order).HasForeignKey<CompletedOrder>(co => co.OrderId);

            base.OnModelCreating(builder);
        }

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
