namespace BetAt.Application.Features.Bet.Commands;

public class UpdateBetCommand : IRequest<UpdateBetDto>
{
    public int Id { get; set; }
    public int PredictedHomeScore { get; set; }
    public int PredictedAwayScore { get; set; }
}