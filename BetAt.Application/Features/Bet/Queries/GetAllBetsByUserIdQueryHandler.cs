using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Bet.Queries;

public class GetAllBetsByUserIdQueryHandler(IBetRepository repository, ICurrentUserService userService) : IRequestHandler<GetAllBetsByUserIdQuery, List<BetDto>>
{
    public async Task<List<BetDto>> Handle(GetAllBetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var bets = await repository.GetAllByUserIdAsync(userService.UserId);

        return bets.Select(bet => bet.ToDto()).ToList();
    }
}