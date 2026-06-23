using inventarySystem_backend.domain.Entities;

namespace inventarySystem_backend.domain.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    void Update(Product product);
    Task DeleteAsync(int id);        
}