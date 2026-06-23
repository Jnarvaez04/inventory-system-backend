using inventarySystem_backend.application.DTOs;

namespace inventarySystem_backend.application.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
    Task<bool> UpdateAsync(int id, CreateCategoryDto dto);
    Task<bool> DeleteAsync(int id);
}
