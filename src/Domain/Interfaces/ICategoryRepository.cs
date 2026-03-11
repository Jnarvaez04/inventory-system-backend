using inventarySystem_backend.src.Domain.Entities;

namespace inventarySystem_backend.src.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        // llamar el listados de todas las categorias
        ICollection<Category> GetCategories();
        // obtener una categoria en especifico
        Category? GetCategory(int id);
        // verificar si una categoria existe por id
        bool CategoryExists(int id);
        // verificar si una categoria existe por nombre
        bool CategoryExists(string name);
        // crear categoria
        bool CreateCategory(Category category);
        // actualizar categoria
        bool UpdateCategory(Category category);
        // eliminar categoria
        bool DeleteCategory(Category category);
        bool Save();
    }
}