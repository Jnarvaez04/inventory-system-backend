using inventarySystem_backend.domain.Entities;

namespace inventarySystem_backend.domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task AddAsync(Category category);
    void Update(Category category); // Update en EF suele ser síncrono en tracking
    Task DeleteAsync(int id);
}
