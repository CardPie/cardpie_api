using AppCore.Attributes;
using AppCore.Models;
using MainData.Entities;

namespace API.Dtos;

public class DeckDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public int View { get; set; }
    public DeckColor Color { get; set; }
    public DeckOrder Order { get; set; }
    public int TotalCard { get; set; }
}

public class DetailDeckDto : BaseDto
{
    public Guid FolderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public int TotalCard { get; set; }
    public DeckColor Color { get; set; }
    public DeckOrder Order { get; set; }
    public Guid UserId { get; set; }
    public int View { get; set; }
    public int? RecallStrength { get; set; }
    public DateTime? ReminderTime { get; set; }
    public int? LearningLength { get; set; }
    public SpacedRepetitionStrategy SpacedRepetitionStrategyLevel { get; set; }
    
    //RelationShip
    public List<FlashCardDto> ListFlashCards { get; set; } = new List<FlashCardDto>();
    public List<StudySessionDto> ListStudySessions { get; set; } = new List<StudySessionDto>();
}

public class CreateDeckDto
{
    public Guid FolderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public DeckColor Color { get; set; }
    public DeckOrder Order { get; set; }
    public int? RecallStrength { get; set; }
    public DateTime? ReminderTime { get; set; }
    public int? LearningLength { get; set; }
    public SpacedRepetitionStrategy SpacedRepetitionStrategyLevel { get; set; }
    public List<CreateFlashCardWithDeckDto> ListFlashCards { get; set; } = new List<CreateFlashCardWithDeckDto>();
}

public class UpdateDeckDto
{
    public Guid? FolderId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsPublic { get; set; }
    public DeckColor? Color { get; set; }
    public DeckOrder? Order { get; set; }
    public int? RecallStrength { get; set; }
    public DateTime? ReminderTime { get; set; }
    public int? LearningLength { get; set; }
    public bool? IsDailyRemind { get; set; }
    public string? WeeklyReminderDays { get; set; }
    public SpacedRepetitionStrategy? SpacedRepetitionStrategyLevel { get; set; }
}


public class CreateFlashCardWithDeckDto
{
    [Required] public string Title { get; set; } 
    [Required] public string Content { get; set; } 
    public string? SoundUrl { get; set; }
    public string? ImageUrl { get; set; } 
    [Required] public string TitleBackOne { get; set; }
    [Required] public string ContentBackOne { get; set; } 
    public string TitleBackTwo { get; set; } 
    public string ContentBackTwo { get; set; } 
    public string? SoundUrlBack { get; set; } 
    public string? ImageUrlBack { get; set; }
}

public class DeckQueryDto : BaseQueryDto
{
}