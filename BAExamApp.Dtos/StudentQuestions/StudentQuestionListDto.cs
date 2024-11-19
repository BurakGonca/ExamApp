namespace BAExamApp.Dtos.StudentQuestions;

public class StudentQuestionListDto
{
    public Guid Id { get; set; }
    public int MaxScore { get; set; }
    public int? Score { get; set; }
    public int QuestionOrder { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }
    public string StudentName { get; set; }
    public string ExamName { get; set;}
}
