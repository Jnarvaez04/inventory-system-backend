

using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Interfaces;
using inventarySystem_backend.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace inventarySystem_backend.infrastructure.Repositories;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private readonly AppDbContext _context;
    public InventoryTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<InventoryTransaction?> GetByIdAsync(int id)
    {
        // Traemos la transacción y cargamos su producto para que Mapster tenga de dónde sacar el ProductName
        return await _context.InventoryTransactions
            .Include(t => t.Product)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(InventoryTransaction transaction)
    {
        await _context.InventoryTransactions.AddAsync(transaction);
    }


    public async Task<IEnumerable<InventoryTransaction>> GetByProductIdAsync(int productId)
    {
        // Usamos .Include(t => t.Product) para que Mapster pueda extraer el ProductName en el DTO
        return await _context.InventoryTransactions
            .Include(t => t.Product)
            .Where(t => t.ProductId == productId)
            .OrderByDescending(t => t.Date) // El historial de transacciones (Kardex) siempre debe ir de la más reciente a la más antigua
            .ToListAsync();
    }
}