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
    public string? CardsStudied { get; set; }
    public int CurrentCardIndex { get; set; }
    public int CorrectCount { get; set; }
    public int IncorrectCount { get; set; }
    public bool IsCompleted { get; set; }
    
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
        builder.Property(a => a.CardsStudied).IsRequired();
        builder.Property(a => a.IsCompleted).IsRequired().HasDefaultValue(false);
    }
}