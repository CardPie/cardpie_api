using AppCore.Models;
using MainData.Entities;

namespace API.Dtos;

public class PostDto : BaseDto
{
    public Guid? FolderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Like { get; set; }
    public int Report { get; set; }
    public PostStatus Status { get; set; }
    public PostType Type { get; set; }
}

public class CreatePostDto
{
    public Guid FolderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public PostType Type { get; set; }
}

public class UpdatePostDto
{
    public string? Content { get; set; } = string.Empty;
    public PostType? Type { get; set; }
}


public class DetailPostDto : BaseDto
{
    public Guid? FolderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Like { get; set; }
    public int Report { get; set; }
    public PostStatus Status { get; set; }
    public PostType Type { get; set; }
    public FolderDto FolderDto { get; set; } = new FolderDto();
    public List<InteractionDto> InteractionDtos { get; set; } = new List<InteractionDto>();
}


public class PostQueryDto : BaseQueryDto
{
    public Guid? FolderId { get; set; }
}