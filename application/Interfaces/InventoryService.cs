using inventarySystem_backend.application.DTOs;

namespace inventarySystem_backend.application.Interfaces;

public interface IInventoryService
{
    Task<TransactionResponseDto?> RegisterStockInAsync(CreateTransactionDto dto);
    Task<TransactionResponseDto?> RegisterStockOutAsync(CreateTransactionDto dto);
    Task<IEnumerable<TransactionResponseDto>> GetHistoryByProductIdAsync(int productId);
    
}