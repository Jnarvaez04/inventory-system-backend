using inventarySystem_backend.application.DTOs;

namespace inventarySystem_backend.application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto?> CreateAsync(CreateProductDto dto); // Retorna null si la categoria no existe
    Task<bool> UpdateAsync(int id, CreateProductDto dto);
    Task<bool> DeleteAsync(int id);
}