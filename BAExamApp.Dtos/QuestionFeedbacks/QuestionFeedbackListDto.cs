namespace BAExamApp.Dtos.QuestionFeedbacks;

public class QuestionFeedbackListDto
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public bool? LikeStatus { get; set; }
}
