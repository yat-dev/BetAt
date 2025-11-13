using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Bet.Queries;

public class GetBetByMatchIdAndUserQueryHandler(IBetRepository repository, ICurrentUserService userService) : IRequestHandler<GetBetByMatchIdAndUserQuery, BetDto?>
{
    public async Task<BetDto?> Handle(GetBetByMatchIdAndUserQuery request, CancellationToken cancellationToken)
    {
        var bet = await repository.GetByIdAsync(request.MatchId, userService.UserId);

        return bet?.ToDto();
    }
}