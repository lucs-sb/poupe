namespace Poupe.API.Mappers;

public static class MappingConfigurations
{
    public static IServiceCollection RegisterMaps(this IServiceCollection services)
    {
        services.RegisterCategoryMaps();
        services.RegisterTransactionMaps();
        services.RegisterUserMaps();

        return services;
    }
}