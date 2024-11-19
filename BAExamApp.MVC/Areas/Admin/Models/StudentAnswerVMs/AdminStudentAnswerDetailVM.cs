using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;

namespace BAExamApp.MVC.Areas.Admin.Models.StudentAnswerVMs;

public class AdminStudentAnswerDetailVM
{
    public Guid Id { get; set; }
    public bool IsSelected { get; set; }
    public bool IsCorrect { get; set; }
    public AdminQuestionDetailsQuestionAnswerVM QuestionAnswer { get; set; }
}
