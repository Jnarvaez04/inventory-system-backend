using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Interfaces;
using Mapster;

namespace inventarySystem_backend.application.Services;
public class CategoryService : ICategoryService
{

    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        return categories.Adapt<IEnumerable<CategoryDto>>();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null) return null;
        
        return category.Adapt<CategoryDto>();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = dto.Adapt<Category>();

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return category.Adapt<CategoryDto>();
    }

    public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if(category == null) return false;

        dto.Adapt(category);

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null) return false;

        await _unitOfWork.Categories.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}