namespace BetAt.Application.Dtos;

public class LeagueMemberDto
{
    public int Id { get; set; }
    public int LeagueId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public MemberRole MemberRole { get; set; } = MemberRole.Member;
    public int Points { get; set; }
    public DateTime CreatedAt { get; set; } 
}