namespace BetAt.Application.Dtos.Bets;

public class GetUserBetsResult
{
    public List<BetDto> Bets { get; set; } = [];
    public BetStatistics Statistics { get; set; } = new();
}