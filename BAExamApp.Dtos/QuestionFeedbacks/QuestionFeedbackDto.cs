namespace BAExamApp.Dtos.QuestionFeedbacks;

public class QuestionFeedbackDto
{
    public string? Comment { get; set; }
    public bool? LikeStatus { get; set; }
    public Guid QuestionId { get; set; }
}
