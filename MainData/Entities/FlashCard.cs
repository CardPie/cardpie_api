using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class FlashCard : BaseEntity
{
    public string Front { get; set; } = string.Empty;
    public string FrontImage { get; set; } = string.Empty;
    public string Back { get; set; } = string.Empty;
    public string BackImage { get; set; } = string.Empty;
    public Guid DeckId { get; set; }
}

public class FlashCardConfig : IEntityTypeConfiguration<FlashCard>
{
    public void Configure(EntityTypeBuilder<FlashCard> builder)
    {
        builder.Property(a => a.Front).IsRequired();
        builder.Property(a => a.FrontImage);
        builder.Property(a => a.Back).IsRequired();
        builder.Property(a => a.BackImage);
        builder.Property(a => a.DeckId).IsRequired().HasDefaultValue(Guid.Empty);
    }
}