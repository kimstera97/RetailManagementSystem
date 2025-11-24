using MediatR;
using RetailManagement.Shared.Dtos;

namespace CentralApp.Handlers.Create;

public class CreateProductRequest(string storeId, ProductDto product) : IRequest<Guid>
{
    public string StoreId { get; set; } = storeId;

    public ProductDto Product { get; set; } = product;
}
