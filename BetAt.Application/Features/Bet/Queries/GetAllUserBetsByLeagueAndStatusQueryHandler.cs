using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Bet.Queries;

public class GetAllUserBetsByLeagueAndStatusQueryHandler(IBetRepository repository, ICurrentUserService userService) : IRequestHandler<GetAllUserBetsByLeagueAndStatusQuery, List<BetDto>>
{
    public async Task<List<BetDto>> Handle(GetAllUserBetsByLeagueAndStatusQuery request, CancellationToken cancellationToken)
    {
        var bets = await repository.GetAllByLeagueIdAndStatusAsync(userService.UserId, request.LeagueId, request.Status);
        
        return bets.Select(bet => bet.ToDto()).ToList();
    }
}