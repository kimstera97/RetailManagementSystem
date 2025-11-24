using CentralApp.Models;
using CentralApp.Repositories;
using MediatR;
using RetailManagement.Shared.Dtos;

namespace CentralApp.Handlers.Create;

public class CreateProductHandler(
    ICentralProductRepository productRepo,
    IStoreRepository storeRepository,
    IHttpClientFactory httpClientFactory,
    ILogger<CreateProductHandler> logger) : IRequestHandler<CreateProductRequest, Guid>
{
    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken ct)
    {
        if (request.Product == null)
        {
            throw new NullReferenceException("EC_Product_Not_Found");
        }
        var entity = new Product
        {
            Id = request.Product.Id == Guid.Empty ? Guid.NewGuid() : request.Product.Id,
            StoreId = request.StoreId,
            Name = request.Product.Name,
            Price = request.Product.Price,
            MinPrice = request.Product.MinPrice,
            Description = request.Product.Description,
        };

        await productRepo.CreateAsync(entity, ct);

        var targetStore = await storeRepository.GetAsync(request.StoreId, ct);

        if (targetStore == null)
        {
            logger.LogWarning($"Store {request.StoreId} not found in DB. Product saved only centrally.");
        }



        if (!string.IsNullOrEmpty(targetStore.ApiUrl))
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var dtoToSend = new ProductDto
                {
                    Id = entity.Id,
                    Name = request.Product.Name,
                    Description = request.Product.Description,
                    Price = request.Product.Price,
                    MinPrice = request.Product.MinPrice
                };

                var response = await client.PostAsJsonAsync($"{targetStore.ApiUrl}/api/sync/receive", dtoToSend, ct);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning($"Failed to sync to store {targetStore.Name}. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Could not sync product {entity.Id} to store {request.StoreId}. Store might be offline.");
            }
        }

        return entity.Id;
    }
}
