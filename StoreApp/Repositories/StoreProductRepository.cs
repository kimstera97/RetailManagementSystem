using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Repositories;

public class StoreProductRepository(StoreDbContext db) : IStoreProductRepository
{
    public async Task CreateAsync(Product entity, CancellationToken cancellationToken)
    {
        await db.Products.AddAsync(entity, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await db.Products.AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await db.Products.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
    {
        db.Update(entity);
        await db.SaveChangesAsync(cancellationToken);
    }
}