using Moq;
using RetailManagement.Shared.Dtos;
using StoreApp.Handlers.Products.Create;
using StoreApp.Models;
using StoreApp.Repositories;
using StoreApp.Services;

namespace RetailManagement.Tests.Handlers.StoreApp;

public class CreateProductHandlerTests
{
    [Fact]
    public async Task Handle_Should_CreateProduct_And_SendToCentral()
    {
        // Arrange
        var mockRepo = new Mock<IStoreProductRepository>();

        var mockMessageService = new Mock<IMessageService>();

        var handler = new CreateProductHandler(mockRepo.Object, mockMessageService.Object);

        var command = new CreateProductRequest
        {
            Name = "Test KitKat",
            Price = 2.50m,
            Description = "Delicious",
            MinPrice = 1.00m
        };

        var resultId = await handler.Handle(command, CancellationToken.None);

        // Assert

        mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);

        mockMessageService.Verify(service => service.SendToCentralAsync(
            It.Is<ProductDto>(p => p.Name == "Test KitKat"),
            "Create",
            It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotEqual(Guid.Empty, resultId);
    }
}
