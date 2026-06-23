namespace inventarySystem_backend.domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Exponemos las interfaces de los repositorios como propiedades de solo lectura
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }


    Task<int> SaveChangesAsync();   
}
