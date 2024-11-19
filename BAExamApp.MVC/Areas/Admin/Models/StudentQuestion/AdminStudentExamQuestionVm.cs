using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.StudentAnswers;
using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;
using BAExamApp.MVC.Areas.Admin.Models.StudentAnswerVMs;

namespace BAExamApp.MVC.Areas.Admin.Models.StudentQuestion;

public class AdminStudentExamQuestionVm
{
    public int MaxScore { get; set; }
    public int BonusScore { get; set; }
    public int? Score { get; set; }
    public int QuestionOrder { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }
    public AdminQuestionExamDetailsVM Question { get; set; }
    public List<AdminStudentAnswerDetailVM> StudentAnswers { get; set; }
}
