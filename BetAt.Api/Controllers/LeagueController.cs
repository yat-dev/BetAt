using BetAt.Application.Dtos;
using BetAt.Application.Features.Leagues.Queries;

namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeagueController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LeagueDto>>> GetAllAsync()
    {
        var leagues = await mediator.Send(new GetAllLeaguesQuery());
        
        return Ok(leagues);
    }

    [HttpGet("allleaguesbyuser")]
    public async Task<ActionResult<List<LeagueDto>>> GetAllByUserAsync()
    {
        var leagues = await mediator.Send(new GetAllLeaguesByUserQuery());
        
        return Ok(leagues);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeagueDto>> GetByIdAsync(int id)
    {
        var league = await mediator.Send(new GetLeagueByIdQuery { Id = id });
        
        if (league == null)
            return NotFound($"League id {id} not found");
            
        return Ok(league);
    }
}