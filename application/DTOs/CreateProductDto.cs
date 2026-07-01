

using System.ComponentModel.DataAnnotations;

namespace inventarySystem_backend.application.DTOs;

public record CreateProductDto
{
    [Required(ErrorMessage = "El SKU es obligatorio.")]
    [StringLength(50, ErrorMessage = "El SKU no puede superar los 50 caracteres.")]
    public required string SKU { get; init; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
    public required string Name { get; init; }

    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
    public string? Description { get; init; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Price { get; init; }

    
    [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negrativo.")]
    public int MinimumStock { get; init; } = 5;

    [Required(ErrorMessage = "La categoría es obligatoria.")]
    public int CategoryId { get; init; }
}
