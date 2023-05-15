using MainData.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainData;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<FlashCard> FlashCards { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new TokenConfig());
        modelBuilder.ApplyConfiguration(new DeckConfig());
        modelBuilder.ApplyConfiguration(new FlashCardConfig());
        modelBuilder.ApplyConfiguration(new StudySessionConfig());
    }
}