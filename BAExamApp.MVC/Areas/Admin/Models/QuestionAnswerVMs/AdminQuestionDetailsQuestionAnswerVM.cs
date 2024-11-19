namespace BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;

public class AdminQuestionDetailsQuestionAnswerVM
{
    public Guid Id { get; set; }
    public string Answer { get; set; }
    public bool IsAnswerImage { get; set; }
    public bool IsRightAnswer { get; set; }
}