namespace BetAt.Application.Dtos;

public class CalculateMatchPointsResultDto
{
    public int MatchId { get; set; }
    public int TotalBetsProcessed { get; set; }
    // public List<BetPointsDto> BetsUpdated { get; set; } = [];
}