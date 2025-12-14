using Mapster;
using Poupe.API.Models.Category;
using Poupe.Domain.DTOs.Category;
using Poupe.Domain.Entities;

namespace Poupe.API.Mappers;

public static class CategoryMappingConfigurations
{
    public static void RegisterCategoryMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateCategoryModel, CategoryCreateDTO>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Purpose, src => src.Purpose);

        TypeAdapterConfig<CategoryCreateDTO, Category>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Purpose, src => src.Purpose);

        TypeAdapterConfig<Category, CategoryResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Purpose, src => src.Purpose);

        TypeAdapterConfig<UpdateCategoryModel, CategoryUpdateDTO>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Purpose, src => src.Purpose);

        TypeAdapterConfig<CategoryUpdateDTO, Category>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Purpose, src => src.Purpose);
    }
}
