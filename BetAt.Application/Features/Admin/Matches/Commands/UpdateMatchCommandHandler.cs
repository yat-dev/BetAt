using BetAt.Application.Dtos.Matches;
using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Admin.Matches.Commands;

public class UpdateMatchCommandHandler(IMatchRepository matchRepository, IBetRepository betRepository) : IRequestHandler<UpdateMatchCommand, MatchDto>
{
    public async Task<MatchDto> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await matchRepository.GetByIdAsync(request.Dto.Id);
        
        if (match == null)
            throw new NotFoundException($"Match {request.Dto.Id} not found.");
        
        if (request.Dto.HomeTeamId == request.Dto.AwayTeamId)
            throw new Exception("Une équipe ne peut pas jouer contre elle-même");
        
        if (match.Status == MatchStatus.Finished && 
            (match.HomeScore != request.Dto.HomeScore || match.AwayScore != request.Dto.AwayScore))
        {
            var hasBets = await betRepository.IsMatchHasBetAsync(match.Id);

            if (hasBets)
                throw new Exception("⚠️ Attention : Ce match a des paris avec des points calculés. Modifier le score nécessitera un recalcul des points.");
            
        }
        
        await matchRepository.UpdateAsync(match);
            
        return new MatchDto
        {
            Id = match.Id,
            HomeTeam = new TeamDto
            {
                Id = match.HomeTeam.Id,
                Name = match.HomeTeam.Name,
                Country = match.HomeTeam.Country,
                LogoUrl = match.HomeTeam.LogoUrl,
                ShortName = match.HomeTeam.ShortName
            },
            AwayTeam = new TeamDto
            {
                Id = match.AwayTeam.Id,
                Name = match.AwayTeam.Name,
                Country = match.AwayTeam.Country,
                LogoUrl = match.AwayTeam.LogoUrl,
                ShortName = match.AwayTeam.ShortName
            },
            Venue = new VenueDto
            {
                Id = match.Venue!.Id,
                Name = match.Venue.Name,
                Country = match.Venue.Country,
                City = match.Venue.City,
                Capacity = match.Venue.Capacity,
                ImageUrl = match.Venue.ImageUrl
            },
            Competition = match.Competition,
            MatchDate = match.MatchDate,
            HomeScore = match.HomeScore,
            AwayScore = match.AwayScore,
            MatchStatus = match.Status,
            StatusLabel = match.Status.ToString(),
            BetCount = betRepository.GetCountByMatchIdAsync(match.Id)
        };
    }
}