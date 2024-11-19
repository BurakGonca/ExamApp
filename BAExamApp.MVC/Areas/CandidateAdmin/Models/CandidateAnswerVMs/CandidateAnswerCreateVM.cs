namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionAnswerVMs;

public class CandidateAnswerCreateVM
{
    public string Answer { get; set; } 
    public bool IsImageAnswer { get; set; }
    public bool IsRightAnswer { get; set; }
    public Guid QuestionId { get; set; }
}
