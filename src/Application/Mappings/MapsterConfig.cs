using inventarySystem_backend.DTOs;
using inventarySystem_backend.src.Domain.Entities;
using Mapster;

public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDto>()
            .Map(dest => dest.CreatedAt, src => src.CreationDate);

        config.NewConfig<CreateCategoryDto, Category>()
            .Map(dest => dest.CreationDate, src => DateTime.UtcNow)
            .Ignore(dest => dest.Id);
    }
}
