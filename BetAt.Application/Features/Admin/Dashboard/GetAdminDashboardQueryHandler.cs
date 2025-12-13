namespace BetAt.Application.Features.Admin.Dashboard;

public class GetAdminDashboardQueryHandler(
    IMatchRepository matchRepository,
    IVenueRepository venueRepository,
    ITeamRepository teamRepository,
    IBetRepository betRepository,
    IUserRepository userRepository,
    ILeagueRepository leagueRepository) : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
{
    public async Task<AdminDashboardDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
    {
        var totalVenues = await venueRepository.GetAllAsync();
        var totalTeams = await teamRepository.GetAllAsync();
        var totalMatches = await matchRepository.GetAllAsync();
        var totalUpcomingMatches = await matchRepository.GetAllUpcomingAsync(14);
        var totalLiveMatches = await matchRepository.GetAllLivesAsync();
        var totalFinishedMatches = await matchRepository.GetAllFinishedMatchesAsync();
        var totalBets = await betRepository.GetAllAsync();
        var totalUsers = await userRepository.GetAllAsync();
        var totalLeagues = await leagueRepository.GetAllLeaguesAsync();

        return new AdminDashboardDto
        {
            TotalVenues = totalVenues.Count,
            TotalTeams = totalTeams.Count,
            TotalMatches = totalMatches.Count,
            UpcomingMatches = totalUpcomingMatches.Count,
            LiveMatches = totalLiveMatches.Count,
            FinishedMatches = totalFinishedMatches.Count,
            TotalBets = totalBets.Count,
            TotalUsers = totalUsers.Count,
            TotalLeagues = totalLeagues.Count
        };
    }
}