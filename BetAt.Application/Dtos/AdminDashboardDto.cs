namespace BetAt.Application.Dtos;

public class AdminDashboardDto
{
    public int TotalVenues { get; set; }
    public int TotalTeams { get; set; }
    public int TotalMatches { get; set; }
    public int UpcomingMatches { get; set; }
    public int LiveMatches { get; set; }
    public int FinishedMatches { get; set; }
    public int TotalBets { get; set; }
    public int TotalUsers { get; set; }
    public int TotalLeagues { get; set; }
}