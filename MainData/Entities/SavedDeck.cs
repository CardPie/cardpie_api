using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class SavedDeck : BaseEntity
{
    public Guid DeckId { get; set; }
    public string Note { get; set; } = string.Empty;
}

public class SavedDeckConfig : IEntityTypeConfiguration<SavedDeck>
{
    public void Configure(EntityTypeBuilder<SavedDeck> builder)
    {
        builder.Property(x => x.DeckId).IsRequired();
        builder.Property(x => x.Note).IsRequired(false);
    }
}