using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Entities.Enums;

namespace BAExamApp.MVC.Areas.Student.Models.StudentQuestionVMs;

public class StudentStudentQuestionDetailsVM
{
    public Guid StudentQuestionId { get; set; }

    public Guid StudentExamId { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public DateTime ExamDateTime { get; set; }
    public int QuestionCount { get; set; }
    public int QuestionOrder { get; set; }
    public string Content { get; set; }
    public QuestionType QuestionType { get; set; }
    public string? Image { get; set; }
    public TimeSpan TimeGiven { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }
    public List<QuestionAnswerDto> QuestionAnswers { get; set; }
}
