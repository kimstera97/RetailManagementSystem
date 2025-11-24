namespace RetailManagement.Shared.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }

    public string StoreId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal MinPrice { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
