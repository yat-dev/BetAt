using BetAt.Application.Dtos.Matches;
using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Admin.Matches.Commands;

public class CreateMatchCommandHandler(IMatchRepository matchRepository, 
                                       ITeamRepository teamRepository, 
                                       IVenueRepository venueRepository,
                                       IBetRepository betRepository) : IRequestHandler<CreateMatchCommand, MatchDto>
{
    public async Task<MatchDto> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        if (request.Dto.HomeTeamId == request.Dto.AwayTeamId)
            throw new Exception("Une équipe ne peut pas jouer contre elle-même");
        
        var homeTeam = await teamRepository.GetByIdAsync(request.Dto.HomeTeamId);
        var awayTeam = await teamRepository.GetByIdAsync(request.Dto.AwayTeamId);
        var venue = await venueRepository.GetByIdAsync(request.Dto.VenueId);
        
        if (homeTeam == null)
            throw new NotFoundException($"Équipe domicile (ID {request.Dto.HomeTeamId}) introuvable");

        if (awayTeam == null)
            throw new NotFoundException($"Équipe extérieur (ID {request.Dto.AwayTeamId}) introuvable");

        if (venue == null)
            throw new NotFoundException($"Stade (ID {request.Dto.VenueId}) introuvable");

        if (await matchRepository.IsMatchExistsAsync(request.Dto.HomeTeamId, request.Dto.AwayTeamId, request.Dto.MatchDate))
            throw new Exception($"Un match {homeTeam.Name} vs {awayTeam.Name} existe déjà à cette date");

        var match = new Match
        {
            HomeTeamId = request.Dto.HomeTeamId,
            AwayTeamId = request.Dto.AwayTeamId,
            VenueId = request.Dto.VenueId,
            Competition = request.Dto.Competition,
            MatchDate = request.Dto.MatchDate,
            Status = MatchStatus.Scheduled,
            CreatedAt = DateTime.UtcNow
        };
        
        var matchAdded = await matchRepository.AddAsync(match);
        
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