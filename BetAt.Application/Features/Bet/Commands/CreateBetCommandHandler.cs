using Microsoft.AspNetCore.Mvc;

namespace BetAt.Application.Features.Bet.Commands;

public class CreateBetCommandHandler(IBetRepository repository, ICurrentUserService userService) : IRequestHandler<CreateBetCommand, int>
{
    public async Task<int> Handle(CreateBetCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Bet bet = new Domain.Entities.Bet
        {
            UserId = userService.UserId,
            MatchId = request.MatchId,
            LeagueId = request.LeagueId,
            PredictedHomeScore = request.PredictedHomeScore,
            PredictedAwayScore = request.PredictedAwayScore
        };
        
        return await repository.AddAsync(bet);
    }
}