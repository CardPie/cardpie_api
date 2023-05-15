using System.Security.Cryptography.X509Certificates;
using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class Deck : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    
    //RelationShip
    public virtual IEnumerable<FlashCard> FlashCards { get; set; } = new List<FlashCard>();
    public virtual IEnumerable<StudySession> StudySessions { get; set; } = new List<StudySession>();
}

public class DeckConfig : IEntityTypeConfiguration<Deck>
{
    public void Configure(EntityTypeBuilder<Deck> builder)
    {
        builder.Property(a => a.UserId).IsRequired().HasDefaultValue(Guid.Empty);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.Description).IsRequired();
        builder.HasMany(a => a.FlashCards);
        builder.HasMany(a => a.StudySessions);
    }
}