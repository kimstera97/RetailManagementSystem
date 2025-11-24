using RetailManagement.Shared.Dtos;

namespace StoreApp.Services;

public interface IMessageService
{
    Task SendToCentralAsync(ProductDto product, string changeType, CancellationToken cancellationToken = default);
}
