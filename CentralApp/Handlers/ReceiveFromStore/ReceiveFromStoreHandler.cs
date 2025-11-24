using CentralApp.Models;
using CentralApp.Repositories;
using MediatR;

namespace CentralApp.Handlers.ReceiveFromStore;

public class ReceiveFromStoreHandler(
    ICentralProductRepository repo,
    IStoreRepository storeRepository) : IRequestHandler<ReceiveFromStoreRequest>
{
    public async Task Handle(ReceiveFromStoreRequest request, CancellationToken ct)
    {
        if (request.Payload == null)
        {
            throw new NullReferenceException("EC_Payload_Not_Found");
        }

        if (request.Payload.Product == null)
        {
            throw new NullReferenceException("EC_Payload_Not_Found");
        }

        var product = request.Payload.Product;
        var storeId = request.Payload.StoreId;

        var targetStore = await storeRepository.GetAsync(storeId, ct);

        if (targetStore == null)
        {
            throw new NullReferenceException("EC_Store_Not_Found");
        }

        var entity = await repo.GetAsync(product.Id, storeId, ct);

        if (request.Payload.OperationType == "create" && entity == null)
        {
            var centralProduct = new Product
            {
                Id = product.Id,
                StoreId = storeId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                MinPrice = product.MinPrice,
            };

            await repo.CreateAsync(centralProduct, ct);
        }
        else if (request.Payload.OperationType == "update" && entity != null)
        {
            entity.Name = product.Name;
            entity.Description = product.Description;
            entity.Price = product.Price;
            entity.MinPrice = product.MinPrice;
            entity.UpdatedOn = DateTime.UtcNow;

            await repo.UpdateAsync(entity, ct);
        }
    }
}
