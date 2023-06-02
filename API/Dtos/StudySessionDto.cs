using AppCore.Models;

namespace API.Dtos;

public class StudySessionDto : BaseDto
{
    public Guid DeckId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? CardsStudied { get; set; }
    public int CurrentCardIndex { get; set; }
    public int CorrectCount { get; set; }
    public int IncorrectCount { get; set; }
    public bool IsCompleted { get; set; }
}

public class StudySessionDetailDto : BaseDto
{
    public Guid DeckId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    //public string? CardsStudied { get; set; }
    public List<FlashCardInSession> ListFlashCards { get; set; } = new List<FlashCardInSession>();
    public int CurrentCardIndex { get; set; }
    public int CorrectCount { get; set; }
    public int IncorrectCount { get; set; }
    public bool IsCompleted { get; set; }
    public DeckDto DeckDto { get; set; } = new DeckDto();
}

public class UpdateStudySessionDto
{
    public List<FlashCardDto> ListFlashCards { get; set; } = new List<FlashCardDto>();
    public int? CurrentCardIndex { get; set; }
    public int? CorrectCount { get; set; }
    public int? IncorrectCount { get; set; }
    public bool? IsCompleted { get; set; }
}

public class CreateStudySessionDto
{
    public Guid DeckId { get; set; }
    //public string? CardsStudied { get; set; }
    public List<FlashCardInSession> ListFlashCards { get; set; } = new List<FlashCardInSession>();
    public int? CurrentCardIndex { get; set; }
    public int? CorrectCount { get; set; }
    public int? IncorrectCount { get; set; }
    public bool? IsCompleted { get; set; }
}

public class FlashCardInSession
{
    public Guid Id { get; set; }
    public string FrontContent { get; set; } = string.Empty;
    public string? FrontDescription { get; set; } = string.Empty;
    public string? FrontSoundUrl { get; set; }
    public string? FrontImageUrl { get; set; }
    public string BackContent { get; set; } = string.Empty;
    public string? BackDescription { get; set; } = string.Empty;
    public string? BackSoundUrl { get; set; } = string.Empty;
    public string? BackImageUrl { get; set; } = string.Empty;
}

public class StudySessionQueryDto : BaseQueryDto
{
}