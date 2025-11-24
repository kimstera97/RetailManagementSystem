using Microsoft.Extensions.Options;
using RetailManagement.Shared.Dtos;
using RetailManagement.Shared.Models;
using StoreApp.Settings;

namespace StoreApp.Services;

public class MessageService(
    HttpClient httpClient,
    ILogger<MessageService> logger,
    IOptions<StoreSettings> settings) : IMessageService
{
    public async Task SendToCentralAsync(ProductDto product, string changeType, CancellationToken ct)
    {
        var myStoreId = settings.Value.StoreId;
        var centralUrl = settings.Value.CentralApiUrl;

        var payload = new SyncPayload(myStoreId, changeType, product);

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{centralUrl}/api/products/receive", payload, ct);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning($"Central sync failed: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not reach Central App");
        }
    }
}
