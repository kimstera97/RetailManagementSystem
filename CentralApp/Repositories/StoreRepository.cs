using CentralApp.Data;
using CentralApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CentralApp.Repositories;

public class StoreRepository(CentralDbContext db) : IStoreRepository
{
    public async Task<Store> GetAsync(string id, CancellationToken cancellationToken)
    {
        return await db.Stores.AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
