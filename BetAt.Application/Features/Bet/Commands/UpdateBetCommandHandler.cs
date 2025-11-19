namespace BetAt.Application.Features.Bet.Commands;

public class UpdateBetCommandHandler(IBetRepository repository) : IRequestHandler<UpdateBetCommand, UpdateBetDto>
{
    public async Task<UpdateBetDto> Handle(UpdateBetCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Bet bet = new()
        {
            Id = request.Id,
            PredictedHomeScore = request.PredictedHomeScore,
            PredictedAwayScore = request.PredictedAwayScore,
            UpdatedAt = DateTime.UtcNow
        };

        await repository.UpdateAsync(bet);

        return new UpdateBetDto
        {
            PredictedHomeScore = bet.PredictedHomeScore,
            PredictedAwayScore = bet.PredictedAwayScore
        };
    }
}