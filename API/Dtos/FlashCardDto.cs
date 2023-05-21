using AppCore.Attributes;
using AppCore.Models;

namespace API.Dtos;

public class FlashCardDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string SoundUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TitleBackOne { get; set; } = string.Empty;
    public string ContentBackOne { get; set; } = string.Empty;
    public string TitleBackTwo { get; set; } = string.Empty;
    public string ContentBackTwo { get; set; } = string.Empty;
    public string SoundUrlBack { get; set; } = string.Empty;
    public string ImageUrlBack { get; set; } = string.Empty;
}

public class DetailFlashCardDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string SoundUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TitleBackOne { get; set; } = string.Empty;
    public string ContentBackOne { get; set; } = string.Empty;
    public string TitleBackTwo { get; set; } = string.Empty;
    public string ContentBackTwo { get; set; } = string.Empty;
    public string SoundUrlBack { get; set; } = string.Empty;
    public string ImageUrlBack { get; set; } = string.Empty;
    public DeckDto DeckDto { get; set; } = new DeckDto();
}

public class CreateFlashCardDto
{
    public Guid DeckId { get; set; }
    [Required] public string Title { get; set; } 
    [Required] public string Content { get; set; } 
    public string? SoundUrl { get; set; }
    public string? ImageUrl { get; set; } 
    [Required] public string TitleBackOne { get; set; }
    [Required] public string ContentBackOne { get; set; } 
    [Required] public string TitleBackTwo { get; set; } 
    [Required] public string ContentBackTwo { get; set; } 
    public string? SoundUrlBack { get; set; } 
    public string? ImageUrlBack { get; set; }
}

public class UpdateFlashCardDto
{
    public string? Title { get; set; } 
    public string? Content { get; set; } 
    public string? SoundUrl { get; set; }
    public string? ImageUrl { get; set; } 
    public string? TitleBackOne { get; set; }
    public string? ContentBackOne { get; set; } 
    public string? TitleBackTwo { get; set; } 
    public string? ContentBackTwo { get; set; } 
    public string? SoundUrlBack { get; set; } 
    public string? ImageUrlBack { get; set; }
}

public class FlashCardQueryDto : BaseQueryDto
{
    
}