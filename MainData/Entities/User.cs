using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class User : BaseEntity  
{
    public string? Fullname { get; set; }
    public UserRole Role { get; set; }
    public string? Avatar { get; set; }
    public  UserStatus Status { get; set; }
    public  AccountType AccountType { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    
    public DateTime? FirstLoginAt { get; set; }
    
    public DateTime? LastLoginAt { get; set; }
}

public enum UserRole
{
    Member = 1, Guest = 2, Admin = 3
}

public enum AccountType
{
    Learning = 1, Teaching = 2
}

public enum UserStatus
{
    Active = 1, InActive = 2 
}

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Fullname).IsRequired(false);
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.Email).IsRequired(false);
        builder.Property(x => x.Avatar).IsRequired(false).HasDefaultValue("https://i.pinimg.com/564x/76/68/4a/76684ac1fccf120998c15dcc094a07ad.jpg");
        builder.Property(x => x.PhoneNumber).IsRequired(false);
        builder.Property(x => x.Address).IsRequired(false);
        builder.Property(x => x.Password).IsRequired(false);
        builder.Property(x => x.Salt).IsRequired(false);
        builder.Property(x => x.FirstLoginAt).IsRequired(false);
        builder.Property(x => x.LastLoginAt).IsRequired(false);
        builder.Property(x => x.AccountType).IsRequired().HasDefaultValue(AccountType.Learning);
    }
}