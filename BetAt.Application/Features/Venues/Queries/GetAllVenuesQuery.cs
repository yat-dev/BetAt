using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Queries;

public class GetAllVenuesQuery : IRequest<List<VenueDto>>
{
    public string? SearchTerm { get; set; }
    public string? Country { get; set; }
}