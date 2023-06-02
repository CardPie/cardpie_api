using AppCore.Attributes;
using AppCore.Models;

namespace API.Dtos;

public class FlashCardDto : BaseDto
{
    public string FrontContent { get; set; } = string.Empty;
    public string? FrontDescription { get; set; } = string.Empty;
    public string? FrontSoundUrl { get; set; }
    public string? FrontImageUrl { get; set; }
    public string BackContent { get; set; } = string.Empty;
    public string? BackDescription { get; set; } = string.Empty;
    public string? BackSoundUrl { get; set; } = string.Empty;
    public string? BackImageUrl { get; set; } = string.Empty;
}

public class DetailFlashCardDto : BaseDto
{
    public Guid DeckId { get; set; }
    public string FrontContent { get; set; } = string.Empty;
    public string? FrontDescription { get; set; } = string.Empty;
    public string? FrontSoundUrl { get; set; }
    public string? FrontImageUrl { get; set; }
    public string BackContent { get; set; } = string.Empty;
    public string? BackDescription { get; set; } = string.Empty;
    public string BackSoundUrl { get; set; } = string.Empty;
    public string BackImageUrl { get; set; } = string.Empty;
    public DeckDto DeckDto { get; set; } = new DeckDto();
}

public class CreateFlashCardDto
{
    [Required] public Guid DeckId { get; set; }
    [Required] public string FrontContent { get; set; } = string.Empty;
    public string? FrontDescription { get; set; } = string.Empty;
    public string? FrontSoundUrl { get; set; }
    public string? FrontImageUrl { get; set; }
    [Required] public string BackContent { get; set; } = string.Empty;
    public string? BackDescription { get; set; } = string.Empty;
    public string? BackSoundUrl { get; set; } = string.Empty;
    public string? BackImageUrl { get; set; } = string.Empty;
}

public class UpdateFlashCardDto
{
    //public Guid? DeckId { get; set; }
    public string? FrontContent { get; set; } = string.Empty;
    public string? FrontDescription { get; set; } = string.Empty;
    public string? FrontSoundUrl { get; set; }
    public string? FrontImageUrl { get; set; }
    public string BackContent { get; set; } = string.Empty;
    public string? BackDescription { get; set; } = string.Empty;
    public string? BackSoundUrl { get; set; } = string.Empty;
    public string? BackImageUrl { get; set; } = string.Empty;
}

public class FlashCardQueryDto : BaseQueryDto
{
    
}