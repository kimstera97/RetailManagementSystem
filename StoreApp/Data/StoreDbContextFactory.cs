using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StoreApp.Data;

public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
{
    public StoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();

        optionsBuilder.UseNpgsql(
            "User ID=dakata;Password=dakata123;Host=localhost;Port=5455;Database=store-db");
        //User ID=dakata;Password=dakata123;Host=localhost;Port=5451;Database=credit-center
        //Server=localhost;Database=StoreDb;Trusted_Connection=True;TrustServerCertificate=True;
        return new StoreDbContext(optionsBuilder.Options);
    }
}
