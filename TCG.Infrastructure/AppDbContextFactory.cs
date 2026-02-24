using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TCG.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=db;Port=5432;Username=postgres;Password=secret;Database=tcg_db"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}