using inventarySystem_backend.application.DTOs;

namespace inventarySystem_backend.application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterUserDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}