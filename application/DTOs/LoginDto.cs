using System.ComponentModel.DataAnnotations;

namespace inventarySystem_backend.application.DTOs;

public record LoginDto
{
    [Required(ErrorMessage = "El correo electronico es obligatorio.")]
    public required string Email { get; init; }
    
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public required string Password { get; init; }
}