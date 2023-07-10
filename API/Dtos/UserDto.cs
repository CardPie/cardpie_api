using AppCore.Models;
using MainData.Entities;

namespace API.Dtos;

public class UserDto : BaseDto
{
    public string? Fullname { get; set; }
    public UserRole Role { get; set; }
    public string? Avatar { get; set; }
    public UserStatus Status { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Username { get; set; }
    public int StudiedCard { get; set; }
    public int CreatedDeck { get; set; }
    public int SavedDeck { get; set; }
}

public class UpdateUserDto
{
    public string? Fullname { get; set; }
    public string? Avatar { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}

public class UserAdminDto : BaseDto
{
  public string? Fullname { get; set; }
  public UserRole Role { get; set; }
  public string? Avatar { get; set; }
  public  UserStatus Status { get; set; }
  public  AccountType AccountType { get; set; }
  public string? Email { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Address { get; set; }
  public DateTime? ActivePremiumDate { get; set; }

  public TypeOfPremium? TypeOfPremium { get; set; }
  public DateTime? FirstLoginAt { get; set; }

  public DateTime? LastLoginAt { get; set; }
}

public class UserQuery : BaseQueryDto
{
  public string? Keyword { get; set; }
}

public class ActiveDto
{
  public Guid UserId { get; set; }

  public TypeOfPremium TypeOfPremium { get; set; }
}
