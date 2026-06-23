namespace inventarySystem_backend.application.DTOs;
public record CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}