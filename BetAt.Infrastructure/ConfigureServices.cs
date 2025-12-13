using BetAt.Infrastructure.Repositories;
using BetAt.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BetAt.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string? connectionString = null)
    {
        var dbConnectionString = connectionString ?? configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<BetAtDbContext>(options =>
            options.UseNpgsql(
                dbConnectionString,
                b => b.MigrationsAssembly(typeof(BetAtDbContext).Assembly.FullName)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILeagueRepository, LeagueRepository>();
        services.AddScoped<ILeagueMemberRepository, LeagueMemberRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IBetRepository, BetRepository>();
        services.AddScoped<IBetRuleRepository, BetRuleRepository>();
        services.AddScoped<IVenueRepository, VenueRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddHostedService<MatchResultProcessorService>();
        
        return services;
    }
}