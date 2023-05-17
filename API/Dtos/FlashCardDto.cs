using AppCore.Models;

namespace API.Dtos;

public class FlashCardDto : BaseDto
{
    public string Front { get; set; } = string.Empty;
    public string FrontImage { get; set; } = string.Empty;
    public string Back { get; set; } = string.Empty;
    public string BackImage { get; set; } = string.Empty;
}

public class CreateFlashCardDto
{
    public string Front { get; set; } = string.Empty;
    public string FrontImage { get; set; } = string.Empty;
    public string Back { get; set; } = string.Empty;
    public string BackImage { get; set; } = string.Empty;
}