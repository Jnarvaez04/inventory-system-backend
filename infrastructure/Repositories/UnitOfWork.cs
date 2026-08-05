using inventarySystem_backend.domain.Interfaces;
using inventarySystem_backend.infrastructure.Data;

namespace inventarySystem_backend.infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _context;

    public ICategoryRepository Categories { get; }
    public IProductRepository Products { get; }
    public IInventoryTransactionRepository InventoryTransactions { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
        InventoryTransactions = new InventoryTransactionRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

}
