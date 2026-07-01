namespace inventarySystem_backend.application.DTOs;

public record ProductDto
{
    public int Id { get; set; }
    public required string SKU { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public int MinimumStock { get; init; }
    public int CategoryId { get; init; }

    //Propiedad aplanada para no exponer toda la entidad Category
    public string? CategoryName { get; init; }
}