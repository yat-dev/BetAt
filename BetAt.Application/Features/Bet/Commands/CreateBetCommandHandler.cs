using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Bet.Commands;

public class CreateBetCommandHandler(IBetRepository repository, ICurrentUserService userService) : IRequestHandler<CreateBetCommand, CreateBetDto>
{
    public async Task<CreateBetDto> Handle(CreateBetCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Bet bet = new Domain.Entities.Bet
        {
            UserId = userService.UserId,
            MatchId = request.MatchId,
            LeagueId = request.LeagueId,
            PredictedHomeScore = request.PredictedHomeScore,
            PredictedAwayScore = request.PredictedAwayScore
        };
        
        await repository.AddAsync(bet);

        return bet.ToCreateDto();
    }
}