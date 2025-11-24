using MediatR;
using RetailManagement.Shared.Dtos;

namespace StoreApp.Handlers.Products.Create;

public class CreateProductRequest : ProductDto,IRequest<Guid>
{
}
