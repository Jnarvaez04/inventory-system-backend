using inventarySystem_backend.domain.Entities;

namespace inventarySystem_backend.domain.Interfaces;

public interface IInventoryTransactionRepository
{
    Task<InventoryTransaction?> GetByIdAsync(int id);
    Task AddAsync(InventoryTransaction transaction);
    Task<IEnumerable<InventoryTransaction>> GetByProductIdAsync(int productId);
}