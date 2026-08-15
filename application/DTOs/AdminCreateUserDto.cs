using System.ComponentModel.DataAnnotations;

namespace inventarySystem_backend.application.DTOs;


public record AdminCreateUserDto
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [StringLength(50)]
    public required string Username { get; init; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress]
    public required string Email { get; init; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6)]
    public required string Password { get; init; }

    [Required(ErrorMessage = "El rol es obligatorio.")]
    public required string Role { get; init; } // Solo lo ejecutara un administrador autenticado
}