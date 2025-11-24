using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CentralApp.Data;

public class CentralDbContextFactory : IDesignTimeDbContextFactory<CentralDbContext>
{
    public CentralDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CentralDbContext>();

        optionsBuilder.UseNpgsql(
            "User ID=dakata;Password=dakata123;Host=localhost;Port=5449;Database=central-db");

        return new CentralDbContext(optionsBuilder.Options);
    }
}
