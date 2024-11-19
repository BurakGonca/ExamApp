namespace BAExamApp.Dtos.QuestionFeedbacks;

public class QuestionFeedbackCreateDto
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public bool? LikeStatus { get; set; }
    public Guid QuestionId { get; set; }
}
