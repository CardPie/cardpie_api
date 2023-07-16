using System.Security.Policy;

namespace API.Dtos;

public class ImportDeckReaderDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public List<ImportCardReaderDto> Cards { get; set; } = new List<ImportCardReaderDto>();
}

public class ImportCardReaderDto
{
    public string FrontContent { get; set; } = string.Empty;
    public string? FrontDescription { get; set; } = string.Empty;
    public string BackContent { get; set; } = string.Empty;
}

public class ImportUserReaderDto
{
    public string? Fullname { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}