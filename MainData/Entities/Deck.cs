using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class Deck : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public DeckColor Color { get; set; }
    public DeckOrder Order { get; set; }
    public Guid? UserId { get; set; }
    
    public int? RecallStrength { get; set; }
    public DateTime? ReminderTime { get; set; }
    public int? LearningLength { get; set; }
    public bool IsDailyRemind { get; set; }
    public string WeeklyReminderDays { get; set; } = string.Empty;
    public SpacedRepetitionStrategy SpacedRepetitionStrategyLevel { get; set; }
    public Guid FolderId { get; set; }
    
    //RelationShip
    public virtual IEnumerable<FlashCard> FlashCards { get; set; } = new List<FlashCard>();
    public virtual IEnumerable<StudySession> StudySessions { get; set; } = new List<StudySession>();
    public virtual Folder Folder { get; set; } = new Folder();
}

public enum SpacedRepetitionStrategy
{
    Closer = 1, Normal = 2, Distant = 3
}

public enum DeckColor
{
    Purple = 1, Pink = 2, Green = 3, Yellow = 4
}

public enum DeckOrder
{
    Front = 1, Back =2 , Random = 3
}

public enum DayInWeek
{
    Monday = 2, Tuesday = 3, Wednesday = 4, Thursday = 5, Friday = 6, Saturday = 7, Sunday = 8
}

public class DeckConfig : IEntityTypeConfiguration<Deck>
{
    public void Configure(EntityTypeBuilder<Deck> builder)
    {
        builder.Property(a => a.UserId).IsRequired().HasDefaultValue(Guid.Empty);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.Description).IsRequired();
        builder.Property(a => a.Color).IsRequired().HasDefaultValue(DeckColor.Purple);
        builder.Property(a => a.Order).IsRequired().HasDefaultValue(DeckOrder.Front);
        builder.Property(a => a.IsPublic).IsRequired().HasDefaultValue(false);
        builder.Property(a => a.RecallStrength).IsRequired(false);
        builder.Property(a => a.ReminderTime).IsRequired(false);
        builder.Property(a => a.LearningLength).IsRequired(false);
        builder.Property(x => x.FolderId).IsRequired();
        builder.Property(x => x.WeeklyReminderDays).IsRequired();
        builder.Property(x => x.IsDailyRemind).IsRequired().HasDefaultValue(false);
        builder.Property(a => a.SpacedRepetitionStrategyLevel).IsRequired()
            .HasDefaultValue(SpacedRepetitionStrategy.Normal);
        /*builder.HasMany(a => a.FlashCards);
        builder.HasMany(a => a.StudySessions);
        builder.HasOne(a => a.Folder);*/
    }
}