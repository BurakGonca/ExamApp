namespace BAExamApp.Dtos.QuestionAnswers;

public class QuestionAnswerCreateDto
{
    public string Answer { get; set; }
    public bool IsRightAnswer { get; set; }
    public bool IsAnswerImage { get; set; }
    public Guid QuestionId { get; set; }
}