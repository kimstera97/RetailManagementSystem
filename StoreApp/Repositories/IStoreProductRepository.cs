using RetailManagement.Shared.Dtos;
using StoreApp.Models;

namespace StoreApp.Repositories;

public interface IStoreProductRepository
{
    Task CreateAsync(Product entity, CancellationToken cancellationToken);

    Task<Product> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateAsync(Product entity, CancellationToken cancellationToken);
}
