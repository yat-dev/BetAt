namespace BetAt.Application.Dtos.Leagues;

public class LeagueMemberStatsDto
{
    public int UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public int CurrentPoints { get; set; }
    public int CurrentRank { get; set; }
    public int PreviousRank { get; set; }
    public int RankChange { get; set; } // Positif = montée, Négatif = descente
    public bool IsNew { get; set; } // Nouveau dans le classement
    
    // Statistiques de performance
    public int TotalBets { get; set; }
    public int WonBets { get; set; }
    public int LostBets { get; set; }
    public decimal WinRate { get; set; } // Pourcentage de réussite
    public decimal AveragePoints { get; set; }
    
    // Derniers résultats (5 derniers paris)
    public List<BetResultDto> LastResults { get; set; } = new();
    
    // Séries
    public int CurrentStreak { get; set; } // Nombre de paris gagnés/perdus d'affilée
    public bool IsWinStreak { get; set; } // True = série de victoires, False = série de défaites
    
    // Scores exacts
    public int ExactScores { get; set; }
    public int CorrectResults { get; set; }
    public int CorrectGoalDifferences { get; set; }
}