using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class StudySession : BaseEntity
{
    public Guid DeckId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int CurrentCardIndex { get; set; }
    public int CorrectCount { get; set; }
    public int IncorrectCount { get; set; }
    public bool IsCompleted { get; set; }
    
    //Relationship
    //public virtual List<FlashCard> FlashCards { get; set; } = new List<FlashCard>();
    public virtual Deck Deck { get; set; } = new Deck();
    public virtual User User { get; set; } = new User();
}

public class StudySessionConfig : IEntityTypeConfiguration<StudySession>
{
    public void Configure(EntityTypeBuilder<StudySession> builder)
    {
        builder.Property(a => a.DeckId).IsRequired();
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.StartTime).IsRequired();
        builder.Property(a => a.EndTime).IsRequired(false);
        builder.Property(a => a.CurrentCardIndex).IsRequired();
        builder.Property(a => a.CorrectCount).IsRequired();
        builder.Property(a => a.IncorrectCount).IsRequired();
        builder.Property(a => a.IsCompleted).IsRequired().HasDefaultValue(false);
        /*builder.HasOne(x => x.Deck)
            .WithMany(d => d.StudySessions)
            .HasForeignKey(a => a.DeckId)
            .OnDelete(DeleteBehavior.Restrict); // Specify Restrict behavior for this relationship

        builder.HasOne(a => a.User)
            .WithMany(u => u.StudySessions)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade); */
    }
}