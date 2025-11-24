using MediatR;
using RetailManagement.Shared.Dtos;

namespace StoreApp.Handlers.Sync.ReceiveFromCentral;

public class ReceiveFromCentralRequest(ProductDto product) : IRequest
{
    public ProductDto Product { get; set; } = product;
}
