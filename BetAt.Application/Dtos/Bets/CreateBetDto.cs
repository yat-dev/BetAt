namespace BetAt.Application.Dtos;

public class CreateBetDto
{
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public int LeagueId { get; set; }
    public int PredictedHomeScore { get; set; }
    public int PredictedAwayScore { get; set; }
}