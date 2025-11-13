namespace BetAt.Application.Features.Bet.Commands;

public class CreateBetCommand : IRequest<int>
{
    public int MatchId { get; set; }
    public int LeagueId { get; set; }
    public int PredictedHomeScore { get; set; }
    public int PredictedAwayScore { get; set; }
}