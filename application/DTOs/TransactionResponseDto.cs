
namespace inventarySystem_backend.application.DTOs;
public record TransactionResponseDto
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public string? ProductName { get; init; } // Aplanado con mapster
    public int Quantity { get; init; }
    public string? Type { get; init; } // Se devolvera como String ("StockIn" o "StockOut") para el frontend
    public string? Reason { get; init; }
    public DateTime Date { get; init; } 

}