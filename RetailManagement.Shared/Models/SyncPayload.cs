using RetailManagement.Shared.Dtos;

namespace RetailManagement.Shared.Models;

public class SyncPayload(string storeId, string operationType, ProductDto product)
{
    public string StoreId { get; set; } = storeId;

    public string OperationType { get; set; } = operationType;

    public ProductDto Product { get; set; } = product;
}
