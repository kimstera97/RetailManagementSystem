using CentralApp.Handlers.Create;
using CentralApp.Models;
using CentralApp.Repositories;
using Moq;
using RetailManagement.Shared.Dtos;

namespace RetailManagement.Tests.Handlers.CentralApp;

public class CentralSyncTests
{
    [Fact]
    public async Task Handle_Should_SaveProduct_When_ReceivedFromStore()
    {
        // Arrange
        var mockRepo = new Mock<ICentralProductRepository>();

        var mockStoreRepo = new Mock<IStoreRepository>();
        var mockHttpClient = new Mock<IHttpClientFactory>();

        var productDto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Snickers",
            Description = "Desc",
            Price = 10,
            MinPrice = 5
        };

        var request = new CreateProductRequest("Billa-Lyulin", productDto);

        var handler = new CreateProductHandler(mockRepo.Object, mockStoreRepo.Object, mockHttpClient.Object, null);

        // Act
        await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepo.Verify(x => x.CreateAsync(
            It.Is<Product>(p => p.Name == "Snickers" && p.StoreId == "Billa-Lyulin"),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}