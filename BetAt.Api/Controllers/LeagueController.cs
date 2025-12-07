using BetAt.Application.Dtos;
using BetAt.Application.Features.Leagues.Commands;
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

    [HttpGet("myleagues")]
    public async Task<ActionResult<List<LeagueDto>>> GetMyLeaguesAsync()
    {
        var leagues = await mediator.Send(new GetMyLeaguesQuery());
        
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
    
    [HttpGet("{id}/member-stats")]
    public async Task<ActionResult<List<LeagueMemberStatsDto>>> GetLeagueMemberStats(int id)
    {
        var query = new GetLeagueMemberStatsQuery { LeagueId = id };
        
        var stats = await mediator.Send(query);
        
        return Ok(stats);
    }

    [HttpPost("create")]
    public async Task<ActionResult<LeagueDto>> CreateAsync([FromBody]CreateLeagueCommand command)
    {
        var league = await mediator.Send(command);
        
        return Ok(league);
    }

    [HttpPost("join")]
    public async Task<ActionResult<LeagueDto>> JoinAsync([FromBody] JoinLeagueCommand command)
    {
        var league = await mediator.Send(command);
        
        return Ok(league);
    }
}