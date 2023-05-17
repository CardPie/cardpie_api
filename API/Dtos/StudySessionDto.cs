using AppCore.Models;

namespace API.Dtos;

public class StudySessionDto : BaseDto
{
    public Guid DeckId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int CurrentCardIndex { get; set; }
    public int CorrectCount { get; set; }
    public int IncorrectCount { get; set; }
    public bool IsCompleted { get; set; }
}