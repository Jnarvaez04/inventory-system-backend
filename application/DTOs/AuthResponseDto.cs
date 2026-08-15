namespace inventarySystem_backend.application.DTOs;

public record AuthResponseDto
{
    public required string Token { get; init; }
    public required string Username { get; init; }
    public required string Role { get; init; }
    public DateTime Expiration { get; init; }
}