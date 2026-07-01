using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Interfaces;
using Mapster;

namespace inventarySystem_backend.application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return products.Adapt<IEnumerable<ProductDto>>();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var products = await _unitOfWork.Products.GetByIdAsync(id);
        if(products == null) return null;

        return products.Adapt<ProductDto>();
    }

    public async Task<ProductDto?> CreateAsync(CreateProductDto dto)
    {
        // Regla de Negocio: Validar que la categoría exista antes de crear el producto
        var categoryExits = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if(categoryExits == null) return null;

        var product = dto.Adapt<Product>();
        product.Stock = 0; // Garantizamos por negocio que el stock inicial siempre sea 0 (Se alimenta por transacciones)

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        // Volvemos a consultar para que Mapster cargue el CategoryName del registro recién guardado
        var createProduct = await _unitOfWork.Products.GetByIdAsync(product.Id);
        return createProduct.Adapt<ProductDto>();
    }

    public async Task<bool> UpdateAsync(int id, CreateProductDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if(product == null) return false;

        // validar que la nueva categoria exista
        var categoryExits = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if(categoryExits == null) return false;

        dto.Adapt(product);

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if(product == null) return false;

        await _unitOfWork.Products.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}