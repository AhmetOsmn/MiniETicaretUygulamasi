using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MiniETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
