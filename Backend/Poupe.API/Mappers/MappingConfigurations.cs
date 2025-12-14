namespace Poupe.API.Mappers;

public static class MappingConfigurations
{
    public static IServiceCollection RegisterMaps(this IServiceCollection services)
    {
        services.RegisterUserMaps();

        return services;
    }
}