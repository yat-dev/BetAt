using BetAt.Application.Dtos;
using BetAt.Application.Features.Matches.Queries;

namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MatchController(ISender mediator) : ControllerBase
{
    [HttpGet("upcoming")]
    public async Task<ActionResult<List<MatchDto>>> GetAllUpcomingAsync(int days)
    {
        var response = await mediator.Send(new GetAllUpcomingMatchesQuery{Days = days});
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MatchDto>> GetMatch(int id)
    {
        var response = await mediator.Send(new GetMatchByIdQuery(){Id = id});
        
        return Ok(response);
    }
}