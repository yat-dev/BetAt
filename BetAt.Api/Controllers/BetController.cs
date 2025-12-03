using System.Diagnostics.CodeAnalysis;
using BetAt.Application.Features.Bet.Commands;
using BetAt.Domain.Enum;

namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BetController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BetDto>>> Get()
    {
        var bets = await mediator.Send(new GetAllBetsByUserIdQuery());
        
        return Ok(bets);
    }

    [HttpGet("my-bets")]
    public async Task<ActionResult<List<BetDto>>> Get([FromQuery] int? leagueId, [FromQuery] BetStatus status = BetStatus.All)
    {
        var bets = await mediator.Send(new GetAllUserBetsByLeagueAndStatusQuery
        {
            LeagueId = leagueId,
            Status = status
        });
        
        return Ok(bets);
    }

    [HttpGet("{matchId}")]
    public async Task<ActionResult<BetDto>> GetBetByMatchIdAndUser(int matchId)
    {
        var bet = await mediator.Send(new GetBetByMatchIdAndUserQuery { MatchId = matchId });
        
        if (bet == null)
            return NotFound($"Aucun pari pour le match {matchId} n'a été trouvé");
        
        return Ok(bet);
    }
    
    [HttpPost]
    public async Task<ActionResult<BetDto>> PlaceBet([FromBody] CreateBetCommand command)
    {
        var bet = await mediator.Send(command);
        
        return Ok(bet);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<BetDto>> UpdateBet(int id, [FromBody] UpdateBetDto betDto)
    {
        
        var bet = await mediator.Send(new UpdateBetCommand()
        {
            Id = id,
            PredictedHomeScore = betDto.PredictedHomeScore,
            PredictedAwayScore = betDto.PredictedAwayScore
        });
        
        return Ok(bet);
    }
}