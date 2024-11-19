namespace BAExamApp.Dtos.QuestionAnswers;

public class QuestionAnswerDto
{
    public Guid Id { get; set; }
    public string Answer { get; set; }
    public bool IsRightAnswer { get; set; }
    public bool IsAnswerImage { get; set; }
    public Guid QuestionId { get; set; }
}