using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetAt.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BetAtDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(BetAtDbContext).Assembly.FullName)));
        
        return services;
    }
}