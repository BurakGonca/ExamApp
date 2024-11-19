using BAExamApp.Dtos.StudentAnswers;

namespace BAExamApp.Dtos.StudentQuestions;

public class StudentQuestionDto
{
    public Guid Id { get; set; }
    public int MaxScore { get; set; }
    public int BonusScore { get; set; }
    public int? Score { get; set; }
    public int QuestionOrder { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }
    public Guid? StudentExamId { get; set; }
    public Guid? QuestionId { get; set; }
    public List<StudentAnswerDto> StudentAnswers { get; set;}

    public Dictionary<string, double> SubtopicPerformances { get; set; }
}
