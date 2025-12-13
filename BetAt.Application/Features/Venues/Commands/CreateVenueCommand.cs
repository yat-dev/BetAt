using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Commands;

public class CreateVenueCommand : IRequest<VenueDto>
{
    public CreateVenueDto Dto { get; set; } = null!;
}