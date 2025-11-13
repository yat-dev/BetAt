using BetAt.Domain.Repositories;
using BetAt.Infrastructure.Repositories;
using BetAt.Infrastructure.Services;
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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILeagueRepository, LeagueRepository>();
        services.AddScoped<ILeagueMemberRepository, LeagueMemberRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IBetRepository, BetRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        return services;
    }
}