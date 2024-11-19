using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionRuleVMs;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamRuleVMs;

public class CandidateExamRuleCreateVM
{
    public ExamQuestionDistribution ExamQuestionDistribution { get; set; } = ExamQuestionDistribution.SameQuestions;

    public virtual ICollection<CandidateQuestionRuleCreateVM> CandidateQuestionRules { get; set; } = new List<CandidateQuestionRuleCreateVM>();
    public int? TestQuestionsCoefficient { get; set; }
    public int? ClassicQuestionsCoefficient { get; set; }
    public int? AlgorithmQuestionsCoefficient { get; set; } 

    public Guid CandidateExamId { get; set; }
}
