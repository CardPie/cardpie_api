using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class Post : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid? FolderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Like { get; set; }
    public int Report { get; set; }
    public PostStatus Status { get; set; }
    public PostType Type { get; set; }
    
}

public enum PostType
{
    Question = 1 
}


public enum PostStatus
{
    Newly = 1, Hot = 2 
}

public class PostConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.FolderId).IsRequired(false);
        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.Like).IsRequired();
        builder.Property(a => a.Report).IsRequired();
        builder.Property(a => a.Status).IsRequired();
        builder.Property(a => a.Type).IsRequired();
    }
}