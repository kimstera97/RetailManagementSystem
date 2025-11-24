using MediatR;
using StoreApp.Models;
using StoreApp.Repositories;
using StoreApp.Services;

namespace StoreApp.Handlers.Products.Create;

public class CreateProductHandler(
    IStoreProductRepository repo,
    IMessageService messageService) : IRequestHandler<CreateProductRequest, Guid>
{
    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            MinPrice = request.MinPrice,
        };

        await repo.CreateAsync(product, cancellationToken);

        await messageService.SendToCentralAsync(product.ToDto(), "create", cancellationToken);

        return product.Id;
    }
}
