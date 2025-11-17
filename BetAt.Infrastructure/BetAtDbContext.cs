namespace BetAt.Infrastructure;

public class BetAtDbContext : DbContext
{
    public BetAtDbContext(DbContextOptions<BetAtDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Match> Matches { get; set; }
    
    public DbSet<Bet> Bets { get; set; }
    
    public DbSet<League> Leagues { get; set; }
    
    public DbSet<LeagueMember> LeagueMembers { get; set; }
    
    public DbSet<BetRule> BetRules { get; set; }
    
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
            entity.Property(e => e.LastLoginAt).HasDefaultValueSql("NOW()");
        });
        
        builder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id);
                
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.ShortName)
                .IsRequired()
                .HasMaxLength(10);
                
            entity.HasIndex(e => e.Name);
                
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(500);
                
            entity.Property(e => e.Country)
                .HasMaxLength(100);
                
            // Index sur ExternalApiId pour les recherches rapides
            entity.HasIndex(e => e.ExternalApiId);
        });

        builder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
    
            entity.Property(e => e.Description)
                .HasMaxLength(500);
    
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20);
    
            entity.HasIndex(e => e.Code)
                .IsUnique();
        });
        
        builder.Entity<LeagueMember>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasIndex(e => new { e.LeagueId, e.UserId })
                .IsUnique();
                
            // Relation avec League
            entity.HasOne(e => e.League)
                .WithMany(l => l.Members)
                .HasForeignKey(e => e.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Relation avec User
            entity.HasOne(e => e.User)
                .WithMany(u => u.LeagueMemberships)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.Property(e => e.Points)
                .HasDefaultValue(0);
        });
        
        builder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id);
                
            entity.Property(e => e.Competition)
                .IsRequired()
                .HasMaxLength(100);
                
            // Index sur MatchDate pour les requêtes de recherche
            entity.HasIndex(e => e.MatchDate);
                
            entity.HasIndex(e => e.Status);
                
            // Index sur ExternalApiId
            entity.HasIndex(e => e.ExternalApiId);
                
            // Relation avec HomeTeam
            entity.HasOne(e => e.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(e => e.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict); // Empêche la suppression d'une équipe si elle a des matchs
                
            // Relation avec AwayTeam
            entity.HasOne(e => e.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(e => e.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<Bet>(entity =>
        {
            entity.HasKey(e => e.Id);
                
            // Contrainte unique : un utilisateur ne peut parier qu'une fois par match dans une ligue
            entity.HasIndex(e => new { e.UserId, e.MatchId, e.LeagueId })
                .IsUnique();
                
            // Relation avec User
            entity.HasOne(e => e.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Relation avec Match
            entity.HasOne(e => e.Match)
                .WithMany(m => m.Bets)
                .HasForeignKey(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Relation avec League
            entity.HasOne(e => e.League)
                .WithMany(l => l.Bets)
                .HasForeignKey(e => e.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.Property(e => e.IsProcessed)
                .HasDefaultValue(false);
                
            // Index pour les requêtes de calcul de points
            entity.HasIndex(e => e.IsProcessed);
            entity.HasIndex(e => new { e.LeagueId, e.MatchId });
        });
        
        builder.Entity<BetRule>(entity =>
        {
            entity.HasKey(e => e.Id);
                
            // Une ligue ne peut avoir qu'une seule règle
            entity.HasIndex(e => e.LeagueId)
                .IsUnique();
                
            // Relation avec League
            entity.HasOne(e => e.League)
                .WithOne(l => l.BetRule)
                .HasForeignKey<BetRule>(e => e.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Valeurs par défaut
            entity.Property(e => e.ExactScorePoints)
                .HasDefaultValue(5);
                
            entity.Property(e => e.CorrectResultPoints)
                .HasDefaultValue(3);
                
            entity.Property(e => e.CorrectGoalDiffPoints)
                .HasDefaultValue(1);
        });
        
        builder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id);
                
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(500);
                
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false);
                
            // Relation avec User
            entity.HasOne(e => e.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Index pour les requêtes de notifications non lues
            entity.HasIndex(e => new { e.UserId, e.IsRead });
            entity.HasIndex(e => e.CreatedAt);
        });

        builder.Entity<Venue>(entity =>
        {
            entity.HasKey(v => v.Id);
        
            entity.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(v => v.City)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(v => v.Capacity)
                .IsRequired();
        });
    }
}