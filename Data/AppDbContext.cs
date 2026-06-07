using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Tournament> Tournaments => Set<Tournament>();

    public DbSet<Bracket> Brackets => Set<Bracket>();

    public DbSet<Match> Matches => Set<Match>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder
    )
    {
       base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Match>()
            .HasOne(x => x.Player1)
            .WithMany()
            .HasForeignKey(x => x.Player1Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(x => x.Player2)
            .WithMany()
            .HasForeignKey(x => x.Player2Id);

        modelBuilder.Entity<Match>()
            .HasOne(x => x.Winner)
            .WithMany()
            .HasForeignKey(x => x.WinnerId);
        
        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Participants)
            .WithMany(u => u.Tournaments);
    }
}