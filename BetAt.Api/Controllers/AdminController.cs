using BetAt.Application.Dtos.Matches;
using BetAt.Application.Dtos.Teams;
using BetAt.Application.Dtos.Venues;
using BetAt.Application.Features.Admin.Dashboard;
using BetAt.Application.Features.Admin.Matches.Commands;
using BetAt.Application.Features.Admin.Matches.Queries;
using BetAt.Application.Features.Teams.Commands;
using BetAt.Application.Features.Teams.Queries;
using BetAt.Application.Features.Venues.Commands;
using BetAt.Application.Features.Venues.Queries;

namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdminController(ISender mediator) : ControllerBase
{
    [HttpGet("dashboard")]
    public async Task<ActionResult<AdminDashboardDto>> GetDashboard()
    {
        var query = new GetAdminDashboardQuery();
        var result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpGet("venues")]
    public async Task<ActionResult<List<VenueDto>>> GetAllVenues(
        [FromQuery] string? searchTerm,
        [FromQuery] string? country)
    {
        var query = new GetAllVenuesQuery
        {
            SearchTerm = searchTerm,
            Country = country
        };
        var result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpGet("venues/{id}")]
    public async Task<ActionResult<VenueDto>> GetVenueById(int id)
    {
        var query = new GetVenueByIdQuery { Id = id };
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost("venues")]
    public async Task<ActionResult<VenueDto>> CreateVenue([FromBody] CreateVenueDto dto)
    {
        var command = new CreateVenueCommand { Dto = dto };
        var result = await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetVenueById), new { id = result.Id }, result);
    }
    
    [HttpPut("venues/{id}")]
    public async Task<ActionResult<VenueDto>> UpdateVenue(int id, [FromBody] UpdateVenueDto dto)
    {
        if (id != dto.Id)
            return BadRequest("L'ID dans l'URL ne correspond pas à l'ID dans le body");

        var command = new UpdateVenueCommand { Dto = dto };
        var result = await mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpDelete("venues/{id}")]
    public async Task<ActionResult> DeleteVenue(int id)
    {
        var command = new DeleteVenueCommand { Id = id };
        await mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpGet("teams")]
    public async Task<ActionResult<List<TeamDto>>> GetAllTeams([FromQuery] string? searchTerm, [FromQuery] string? country)
    {
        var query = new GetAllTeamsQuery
        {
            SearchTerm = searchTerm,
            Country = country
        };
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("teams/{id}")]
    public async Task<ActionResult<TeamDto>> GetTeamById(int id)
    {
        var query = new GetTeamByIdQuery { Id = id };
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost("teams")]
    public async Task<ActionResult<TeamDto>> CreateTeam([FromBody] CreateTeamDto dto)
    {
        var command = new CreateTeamCommand { Dto = dto };
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTeamById), new { id = result.Id }, result);
    }
    
    [HttpPut("teams/{id}")]
    public async Task<ActionResult<TeamDto>> UpdateTeam(int id, [FromBody] UpdateTeamDto dto)
    {
        if (id != dto.Id)
            return BadRequest("L'ID dans l'URL ne correspond pas à l'ID dans le body");

        var command = new UpdateTeamCommand { Dto = dto };
        var result = await mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpDelete("teams/{id}")]
    public async Task<ActionResult> DeleteTeam(int id)
    {
        var command = new DeleteTeamCommand { Id = id };
        await mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpGet("matches")]
    public async Task<ActionResult<List<MatchDto>>> GetAllMatches(
        [FromQuery] string? competition,
        [FromQuery] int? status,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate)
    {
        var query = new GetAllMatchesAdminQuery
        {
            Competition = competition,
            Status = status.HasValue ? (Domain.Enum.MatchStatus)status.Value : null,
            FromDate = fromDate,
            ToDate = toDate
        };
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("matches/{id}")]
    public async Task<ActionResult<MatchDto>> GetMatchById(int id)
    {
        var query = new GetMatchByIdAdminQuery { Id = id };
        var result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpPost("matches")]
    public async Task<ActionResult<MatchDto>> CreateMatch([FromBody] CreateMatchDto dto)
    {
        var command = new CreateMatchCommand { Dto = dto };
        var result = await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetMatchById), new { id = result.Id }, result);
    }
    
    [HttpPut("matches/{id}")]
    public async Task<ActionResult<MatchDto>> UpdateMatch(int id, [FromBody] UpdateMatchDto dto)
    {
        if (id != dto.Id)
            return BadRequest("L'ID dans l'URL ne correspond pas à l'ID dans le body");

        var command = new UpdateMatchCommand { Dto = dto };
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("matches/{id}")]
    public async Task<ActionResult> DeleteMatch(int id)
    {
        var command = new DeleteMatchCommand { Id = id };
        await mediator.Send(command);
        
        return NoContent();
    }
}