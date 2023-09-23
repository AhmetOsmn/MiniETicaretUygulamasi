using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MiniETicaretAPI.Persistence.Contexts;

namespace MiniETicaretAPI.Persistence
{
    // dotnet cli uzerinden calisacagimiz zaman kullanilir.
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiniETicaretAPIDbContext>
    {
        public MiniETicaretAPIDbContext CreateDbContext(string[] args)
        {


            DbContextOptionsBuilder<MiniETicaretAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
