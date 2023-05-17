using AppCore.Models;
using MainData.Entities;

namespace API.Dtos;

public class DeckDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public DeckColor Color { get; set; }
    public DeckOrder Order { get; set; }
    public int TotalCard { get; set; }
}

public class DetailDeckDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public DeckColor Color { get; set; }
    public DeckOrder Order { get; set; }
    public Guid UserId { get; set; }
    
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
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public DeckColor Color { get; set; }
    public DeckOrder Order { get; set; }
    public int? RecallStrength { get; set; }
    public DateTime? ReminderTime { get; set; }
    public int? LearningLength { get; set; }
    public SpacedRepetitionStrategy SpacedRepetitionStrategyLevel { get; set; }
    public List<CreateFlashCardDto> ListFlashCards { get; set; } = new List<CreateFlashCardDto>();
}

public class DeckQueryDto : BaseQueryDto
{
}