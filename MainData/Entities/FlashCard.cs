using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class FlashCard : BaseEntity
{
    public Guid DeckId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string SoundUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TitleBackOne { get; set; } = string.Empty;
    public string ContentBackOne { get; set; } = string.Empty;
    public string TitleBackTwo { get; set; } = string.Empty;
    public string ContentBackTwo { get; set; } = string.Empty;
    public string SoundUrlBack { get; set; } = string.Empty;
    public string ImageUrlBack { get; set; } = string.Empty;

    //Relationship
    public virtual Deck Deck { get; set; } = new Deck();
}

public class FlashCardConfig : IEntityTypeConfiguration<FlashCard>
{
    public void Configure(EntityTypeBuilder<FlashCard> builder)
    {
        builder.Property(a => a.Title).IsRequired();
        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.ImageUrl).IsRequired();
        builder.Property(a => a.SoundUrl).IsRequired(false);
        builder.Property(a => a.TitleBackOne).IsRequired();
        builder.Property(a => a.ContentBackOne).IsRequired();
        builder.Property(a => a.TitleBackTwo).IsRequired();
        builder.Property(a => a.ContentBackTwo).IsRequired();
        builder.Property(a => a.ImageUrlBack).IsRequired();
        builder.Property(a => a.SoundUrlBack).IsRequired(false);
        builder.Property(a => a.DeckId).IsRequired().HasDefaultValue(Guid.Empty);
    }
}