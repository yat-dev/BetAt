using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Queries;

public class GetVenueByIdQuery : IRequest<VenueDto>
{
    public int Id { get; set; }
}