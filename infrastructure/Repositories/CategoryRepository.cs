using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Interfaces;
using inventarySystem_backend.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace inventarySystem_backend.infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{

    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _db.Categories.ToListAsync();
    }
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task AddAsync(Category category)
    {
        await _db.Categories.AddAsync(category);
    }
    public void Update(Category category)
    {
        _db.Categories.Update(category);
    }
    public async Task DeleteAsync(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category != null)
        {
            _db.Categories.Remove(category);            
        }
    }
}
