namespace BAExamApp.Dtos.StudentQuestions;
public class StudentQuestionCreateDto
{
    public int QuestionOrder { get; set; }
    public Guid? StudentExamId { get; set; }
    public Guid? QuestionId { get; set; }
}
