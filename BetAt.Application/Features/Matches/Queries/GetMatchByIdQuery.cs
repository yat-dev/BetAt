namespace BetAt.Application.Features.Matches.Queries;

public class GetMatchByIdQuery : IRequest<MatchDto>
{
    public int Id { get; set; }
}