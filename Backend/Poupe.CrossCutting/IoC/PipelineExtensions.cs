using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poupe.Application.Services;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories;
using Poupe.Domain.Interfaces.Repositories.Base;
using Poupe.Domain.Settings;
using Poupe.Infrastructure.Auth;
using Poupe.Infrastructure.Repositories;
using Poupe.Infrastructure.Repositories.Base;

namespace Poupe.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void AddAInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
    }

    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
    }
}
