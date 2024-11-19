namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateCandidateAnswerListVM
{
    public Guid Id { get; set; }
    public string? Answer { get; set; }
    public bool? IsRightAnswer { get; set; }
    public string? AIAssessment { get; set; }

    // NAV PROP
    public Guid CandidateQuestionId { get; set; }
    public Guid? CandidateAnswerId { get; set; }
}
