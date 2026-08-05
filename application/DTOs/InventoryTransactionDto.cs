

using System.ComponentModel.DataAnnotations;

namespace inventarySystem_backend.application.DTOs;
public record CreateTransactionDto
{
    [Required(ErrorMessage = "El ID del producto es obligatorio.")]
    public int ProductId { get; init; }
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser como minimo 1.")]
    public int Quantity { get; init; }
    [StringLength(255, ErrorMessage = "La razón no puede superar los 255 caracteres.")]
    public string? Reason { get; init; }
}