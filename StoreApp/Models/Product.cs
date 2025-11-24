using RetailManagement.Shared.Dtos;

namespace StoreApp.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal MinPrice { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

    public ProductDto ToDto() => new ()
    {
        Id = Id,
        Name = Name,
        Description = Description,
        Price = Price,
        MinPrice = MinPrice,
        CreatedOn = CreatedOn,
        UpdatedOn = UpdatedOn,
    };
}
