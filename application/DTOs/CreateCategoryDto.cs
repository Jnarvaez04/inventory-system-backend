using System.ComponentModel.DataAnnotations;

namespace inventarySystem_backend.application.DTOs;
public record CreateCategoryDto
{
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public required string Name { get; init; }
    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
    public string? Description { get; init; }
}