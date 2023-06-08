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