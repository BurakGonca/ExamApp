namespace BAExamApp.Dtos.QuestionAnswers;
public class QuestionAnswerUpdateDto
{
    public Guid Id { get; set; }
    public string Answer { get; set; }
    public bool IsRightAnswer { get; set; }
    public Guid QuestionId { get; set; }
}
