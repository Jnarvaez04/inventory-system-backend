using System.ComponentModel.DataAnnotations;


namespace inventarySystem_backend.application.DTOs;

public record RegisterUserDto
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre de usuario no puede superar los 50 carateres.")]
    public required string Username { get; init; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public required string Password { get; init; }

    [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
    [Compare(nameof(Password), ErrorMessage = "Las contraseñas con coinciden")]
    public required string ConfirmPassword { get; init; }

}