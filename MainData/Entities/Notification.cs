using AppCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    
    //Relationship
    //public virtual User User { get; set; } = new User();
}

public enum NotificationType
{
    NotSeen = 1, Seen =2
}

public class NotificationConfig : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.Type).IsRequired();
    }
}