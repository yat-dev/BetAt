using BetAt.Application.Dtos;
using BetAt.Application.Features.LeagueMembers.Commands;
using BetAt.Application.Features.LeagueMembers.Queries;

namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeagueMemberController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LeagueMemberDto>>> GetAllAsync()
    {
        var response = await mediator.Send(new GetAllLeagueMembersQuery());
        
        return Ok(response);
    }

    [HttpGet("leaguescount")]
    public async Task<ActionResult<int>> GetAllByUserAsync()
    {
        int response = await mediator.Send(new GetAllLeaguesMemberByUserIdQuery());
        
        return Ok(response);
    }

    [HttpGet("points")]
    public async Task<ActionResult<int>> GetAllPointsByUserAsync()
    {
        int response = await mediator.Send(new GetAllLeagueMemberPointsByUserIdQuery());
        
        return Ok(response);
    }

    [HttpDelete("leave/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<NoContentResult> LeaveAsync([FromRoute] int id)
    {
        await mediator.Send(new DeleteLeagueMemberCommand { LeagueId = id });
        
        return NoContent();
    }
}