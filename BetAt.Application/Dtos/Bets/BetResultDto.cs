namespace BetAt.Application.Dtos.Bets;

public class BetResultDto
{
    public bool IsWon { get; set; }
    public int Points { get; set; }
    public DateTime BetDate { get; set; }
}