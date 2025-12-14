using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poupe.Domain.Settings;

namespace Poupe.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PoupeDatabaseSettings>(configuration.GetSection("PoupeDatabase"));
    }
}
