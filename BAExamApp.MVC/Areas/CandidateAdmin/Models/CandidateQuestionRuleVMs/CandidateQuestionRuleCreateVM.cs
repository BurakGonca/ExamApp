namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionRuleVMs;

public class CandidateQuestionRuleCreateVM
{
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public int QuestionCount { get; set; }
    public Guid CandidateQuestionSubjectId { get; set; }
    public Guid CandidateExamRuleId { get; set; }
}
