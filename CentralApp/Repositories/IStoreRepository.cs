using CentralApp.Models;

namespace CentralApp.Repositories;

public interface IStoreRepository
{
    Task<Store> GetAsync(string id, CancellationToken cancellationToken);
}
