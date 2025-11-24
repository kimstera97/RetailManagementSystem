using MediatR;
using StoreApp.Models;
using StoreApp.Repositories;

namespace StoreApp.Handlers.Sync.ReceiveFromCentral;

public class ReceiveFromCentralHandler(IStoreProductRepository repository)
    : IRequestHandler<ReceiveFromCentralRequest>
{
    public async Task Handle(ReceiveFromCentralRequest request, CancellationToken cancellationToken)
    {
        if (request.Product == null)
        {
            throw new NullReferenceException("EC_Product_Not_Found");
        }

        var incomingDto = request.Product;

        var existingEntity = await repository.GetAsync(incomingDto.Id, cancellationToken);

        if (existingEntity == null)
        {
            var newProduct = new Product
            {
                Id = incomingDto.Id,
                Name = incomingDto.Name,
                Description = incomingDto.Description,
                Price = incomingDto.Price,
                MinPrice = incomingDto.MinPrice,
            };

            await repository.CreateAsync(newProduct, cancellationToken);
        }
        else
        {
            existingEntity.Name = incomingDto.Name;
            existingEntity.Description = incomingDto.Description;
            existingEntity.Price = incomingDto.Price;
            existingEntity.MinPrice = incomingDto.MinPrice;
            existingEntity.UpdatedOn = DateTime.UtcNow;

            await repository.UpdateAsync(existingEntity, cancellationToken);
        }
    }
}
