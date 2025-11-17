using BetAt.Application.Common.Exceptions;
using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Matches.Queries;

public class GetMatchByIdQueryHandler(IMatchRepository repository) : IRequestHandler<GetMatchByIdQuery, MatchDto>
{
    public async Task<MatchDto> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
    {
        var match = await repository.GetByIdAsync(request.Id);

        if (match == null)
            throw new NotFoundException(nameof(Match), request.Id);
        
        return match.ToDto();
    }
}