namespace BAExamApp.Dtos.StudentAnswers;
public class StudentAnswerDto
{
    public Guid Id { get; set; }
    public bool IsSelected { get; set; }
    public bool IsCorrect { get; set; }
    public Guid QuestionAnswerId { get; set; }
    public Guid StudentQuestionId { get; set; }
}

