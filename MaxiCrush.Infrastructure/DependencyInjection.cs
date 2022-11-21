using MaxiCrush.Application.Common.Interfaces.Authentication;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MaxiCrush.Infrastructure.Authentication;
using MaxiCrush.Infrastructure.Persistance;
using MaxiCrush.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaxiCrush.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MaxiCrushDbContext>(x =>
        {
            var connectionString = configuration.GetConnectionString("MaxiCrushDb");
            x.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(MaxiCrushDbContext).Assembly.FullName));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        return services;
    }
}
