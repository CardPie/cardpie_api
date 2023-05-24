using AppCore.Attributes;
using AppCore.Models;

namespace API.Dtos;

public class FolderDto : BaseDto
{
    public string FolderName { get; set; } = string.Empty;
    public int TotalDeck { get; set; }
    public int TotalCard { get; set; }
}

public class DetailFolderDto : BaseDto
{
    public string FolderName { get; set; } = string.Empty;
    public int TotalDeck { get; set; }
    public int TotalCard { get; set; }
    public List<DeckDto>? ListDeck { get; set; }
}

public class CreateFolder
{
    [Required(ErrorMessage = "Please insert folder name")]
    public string FolderName { get; set; }
    [Required(ErrorMessage = "Please choose the status")]
    public bool IsPublic { get; set; }
}

public class UpdateFolder
{
    public string? FolderName { get; set; }
    public bool? IsPublic { get; set; }
}

public class FolderQuery : BaseQueryDto
{
}