using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionRuleVMs;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamRuleVMs;

public class CandidateExamRuleVM
{
    public Guid Id { get; set; }

    public ExamQuestionDistribution ExamQuestionDistribution { get; set; }

    public virtual ICollection<CandidateQuestionRuleVM> CandidateQuestionRules { get; set; }

    public Guid CandidateExamId { get; set; }
}
