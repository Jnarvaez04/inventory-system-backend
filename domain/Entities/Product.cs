namespace inventarySystem_backend.domain.Entities;
public class Product
{
    public int Id { get; set; }
    public required string SKU { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;
    public int MinimumStock { get; set; } = 5;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    public int CategoryId { get; set; }
    public Category? Category { get; set; }
        
}
