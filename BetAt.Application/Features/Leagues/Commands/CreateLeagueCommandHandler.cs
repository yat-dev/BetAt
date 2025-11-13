using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Leagues.Commands;

public class CreateLeagueCommandHandler(ILeagueRepository leagueRepository,
    ILeagueMemberRepository leagueMemberRepository,
    IBetRuleRepository betRuleRepository, ICurrentUserService currentUserService) : IRequestHandler<CreateLeagueCommand, LeagueDto>
{
    public async Task<LeagueDto> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
    {
        League league = new League
        {
            Name = request.Name,
            Code = await GenerateUniqueCodeAsync(),
            Description = request.Description,
            CreatedById = currentUserService.UserId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        
        var createdLeague = await leagueRepository.AddAsync(league);

        var leagueMember = new LeagueMember
        {
            LeagueId = league.Id,
            UserId = currentUserService.UserId,
            Role = MemberRole.Admin,
            JoinedAt = DateTime.UtcNow,
            Points = 0,
            CreatedAt = DateTime.UtcNow
        };
        
        await leagueMemberRepository.AddAsync(leagueMember);
        
        var betRule = new BetRule
        {
            LeagueId = league.Id,
            ExactScorePoints = request.ExactScorePoints,
            CorrectResultPoints = request.CorrectResultPoints,
            CorrectGoalDiffPoints = request.CorrectGoalDiffPoints
        };
        
        await betRuleRepository.AddAsync(betRule);
        
        return league.ToDto();
    }
    
    
    private async Task<string> GenerateUniqueCodeAsync()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        string code;
        
        do
        {
            code = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        while (await leagueRepository.CodeExistsAsync(code));
        
        return code;
    }
}