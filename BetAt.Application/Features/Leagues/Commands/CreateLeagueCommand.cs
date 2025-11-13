namespace BetAt.Application.Features.Leagues.Commands;

public class CreateLeagueCommand : IRequest<LeagueDto>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ExactScorePoints { get; set; } = 5;
    public int CorrectResultPoints { get; set; } = 3;
    public int CorrectGoalDiffPoints { get; set; } = 1;
}