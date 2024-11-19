namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamQuestionsByCandidateAnswersListVM
{
    public Guid Id { get; set; }
    public string Answer { get; set; }
    public bool IsImageAnswer { get; set; }
    public bool IsRightAnswer { get; set; }

}
