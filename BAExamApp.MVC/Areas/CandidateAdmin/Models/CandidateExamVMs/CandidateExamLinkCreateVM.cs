using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamRuleVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionRuleVMs;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamLinkCreateVM
{
    public CandidateExamCreateVM CandidateExamCreateVM { get; set; }
    public List<CandidateQuestionRuleCreateVM> CandidateQuestionRuleCreateVM { get; set; }
    public CandidateExamRuleCreateVM CandidateExamRuleCreateVM { get; set; }
}
