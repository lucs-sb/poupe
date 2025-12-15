using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poupe.Domain.Settings;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories;
using Poupe.Domain.Interfaces.Repositories.Base;
using Poupe.Application.Services;
using Poupe.Infrastructure.Repositories.Base;
using Poupe.Infrastructure.Repositories;

namespace Poupe.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();
    }

    public static void AddAInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITransactionRepository, TransactionRepository>();
    }
    //public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.Configure<PoupeDatabaseSettings>(configuration.GetSection("PoupeDatabase"));
    //}
}
