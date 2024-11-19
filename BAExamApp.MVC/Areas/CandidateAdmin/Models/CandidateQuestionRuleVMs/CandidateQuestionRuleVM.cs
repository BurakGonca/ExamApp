using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionRuleVMs;

public class CandidateQuestionRuleVM
{
    public Guid id { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public int QuestionCount { get; set; }
    public Guid CandidateQuestionSubjectId { get; set; }
    public Guid CandidateExamRuleId { get; set; }
}
