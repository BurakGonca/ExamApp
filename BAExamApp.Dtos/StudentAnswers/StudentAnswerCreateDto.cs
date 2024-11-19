namespace BAExamApp.Dtos.StudentAnswers;
public class StudentAnswerCreateDto
{
    public bool IsSelected { get; set; }
    public Guid QuestionAnswerId { get; set; }
    public Guid StudentQuestionId { get; set; }
   
}
