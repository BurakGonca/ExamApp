using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.StudentAnswers;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.StudentQuestions;
public class StudentQuestionDetailsDto
{
    public Guid Id { get; set; }
    public int QuestionOrder { get; set; }
    public string Content { get; set; }
    public QuestionType QuestionType { get; set; }
    public int MaxScore { get; set; }
    public int BonusScore { get; set; }
    public string? Image { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan TimeGiven { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }
    public Guid? StudentExamId { get; set; }
    public Guid? QuestionId { get; set; }
    public List<QuestionAnswerDto> QuestionAnswers { get; set; }
    public List<StudentAnswerDto> StudentAnswers { get; set; }
}
