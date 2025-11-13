using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Leagues.Commands;

public class JoinLeagueCommandHandler(
    ILeagueRepository leagueRepository,
    ILeagueMemberRepository leagueMemberRepository,
    ICurrentUserService userService) : IRequestHandler<JoinLeagueCommand, LeagueDto>
{
    public async Task<LeagueDto> Handle(JoinLeagueCommand request, CancellationToken cancellationToken)
    {
        var league = await leagueRepository.GetLeagueByCodeAsync(request.Code.ToUpper());
        
        if (league == null)
            throw new Exception($"Aucune ligue trouvée avec le code {request.Code}");
        
        if (!league.IsActive)
            throw new Exception("Cette ligue n'est plus active");
        
        // Vérifier si l'utilisateur est déjà membre
        var existingMember = await leagueMemberRepository.GetByUserAndLeagueAsync(
            userService.UserId, 
            league.Id);
        
        if (existingMember != null)
            throw new Exception("Vous êtes déjà membre de cette ligue");
        
        // Ajouter l'utilisateur comme membre
        var member = new LeagueMember
        {
            LeagueId = league.Id,
            UserId = userService.UserId,
            Role = MemberRole.Member,
            JoinedAt = DateTime.UtcNow,
            Points = 0
        };
        
        await leagueMemberRepository.AddAsync(member);

        return league.ToDto();
    }
}