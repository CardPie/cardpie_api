using AppCore.Models;
using MainData.Entities;

namespace API.Dtos;

public class PostDto : BaseDto
{
    public Guid UserId { get; set; }
    public Guid? FolderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Like { get; set; }
    public int Report { get; set; }
    public PostStatus Status { get; set; }
    public PostType Type { get; set; }
}

public class PostQueryDto : BaseQueryDto
{
    public Guid? FolderId { get; set; }
}