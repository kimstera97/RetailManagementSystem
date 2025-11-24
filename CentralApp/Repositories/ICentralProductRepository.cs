using CentralApp.Models;
namespace CentralApp.Repositories;

public interface ICentralProductRepository
{
    Task CreateAsync(Product entity, CancellationToken cancellationToken);

    Task<Product> GetAsync(Guid id, string storeId, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateAsync(Product entity, CancellationToken cancellationToken);
}
