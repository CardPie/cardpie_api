using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class FlashCard : BaseEntity
{
    public Guid DeckId { get; set; }
    public string FrontContent { get; set; } = string.Empty;
    public string? FrontDescription { get; set; } = string.Empty;
    public string? FrontSoundUrl { get; set; }
    public string? FrontImageUrl { get; set; }
    public string BackContent { get; set; } = string.Empty;
    public string? BackDescription { get; set; } = string.Empty;
    public string? BackSoundUrl { get; set; } = string.Empty;
    public string? BackImageUrl { get; set; } = string.Empty;
}

public class FlashCardConfig : IEntityTypeConfiguration<FlashCard>
{
    public void Configure(EntityTypeBuilder<FlashCard> builder)
    {
        builder.Property(a => a.FrontContent).IsRequired();
        builder.Property(a => a.FrontImageUrl).IsRequired(false);
        builder.Property(a => a.FrontSoundUrl).IsRequired(false);
        builder.Property(a => a.FrontDescription).IsRequired(false);
        builder.Property(a => a.BackContent).IsRequired();
        builder.Property(a => a.BackDescription).IsRequired(false);
        builder.Property(a => a.BackImageUrl).IsRequired(false);
        builder.Property(a => a.BackSoundUrl).IsRequired(false);
        builder.Property(a => a.DeckId).IsRequired().HasDefaultValue(Guid.Empty);
    }
}