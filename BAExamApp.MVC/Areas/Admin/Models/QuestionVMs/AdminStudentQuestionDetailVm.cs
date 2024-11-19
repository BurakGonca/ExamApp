using BAExamApp.MVC.Areas.Admin.Models.ExamVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

public class AdminStudentQuestionDetailVm
{
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public string SubtopicName { get; set; }
    public string Image { get; set; }
    public List<AdminQuestionDetailsQuestionAnswerVM> Answers { get; set; }

}
