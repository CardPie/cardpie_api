using AppCore.Models;
using MainData.Entities;

namespace API.Dtos;

public class InteractionDto : BaseDto
{
    public Guid PostId { get; set; }
    public InteractionType Type { get; set; }
    public string? Content { get; set; }
}

public class DetailInteractionDto : BaseDto
{
    public Guid PostId { get; set; }
    public InteractionType Type { get; set; }
    public string? Content { get; set; }

    public PostDto PostDto { get; set; } = new PostDto();
}


public class UpdateInteractionDto
{
    public string? Content { get; set; }
}

public class CreateInteractionDto
{
    public Guid PostId { get; set; }
    public InteractionType Type { get; set; }
    public string? Content { get; set; }
}

public class InteractionQueryDto : BaseQueryDto
{
    
}