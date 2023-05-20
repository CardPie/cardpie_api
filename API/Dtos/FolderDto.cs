using AppCore.Models;

namespace API.Dtos;

public class FolderDto : BaseDto
{
    public string FolderName { get; set; } = string.Empty;
}

public class FolderQuery : BaseQueryDto
{
    
}