using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Commands;

public class UpdateVenueCommand : IRequest<VenueDto>
{
    public UpdateVenueDto Dto { get; set; } 
}