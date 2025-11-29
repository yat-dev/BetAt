namespace BetAt.Application.Features.Bet.Commands;

public class CalculateMatchPointsCommand : IRequest<CalculateMatchPointsResultDto>
{
    public int MatchId { get; set; }
}