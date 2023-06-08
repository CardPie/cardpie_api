using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class Interaction : BaseEntity
{
    public Guid PostId { get; set; }
    public InteractionType Type { get; set; }
    public string? Content { get; set; }
    
}

public enum InteractionType
{
    Like = 1, Report = 2, Comment = 3
}

public class InteractionConfig : IEntityTypeConfiguration<Interaction>
{
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.Property(a => a.PostId).IsRequired();
        builder.Property(a => a.Content).IsRequired(false);
        builder.Property(a => a.Type).IsRequired();
    }
}