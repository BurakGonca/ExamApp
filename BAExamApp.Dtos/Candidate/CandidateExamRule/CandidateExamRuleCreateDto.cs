using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExamRule;
public class CandidateExamRuleCreateDto
{

    public ExamQuestionDistribution ExamQuestionDistribution { get; set; }

    public virtual ICollection<CandidateQuestionRuleCreateDto> CandidateQuestionRules { get; set; }
    public int TestQuestionsCoefficient { get; set; }
    public int ClassicQuestionsCoefficient { get; set; }
    public int AlgorithmQuestionsCoefficient { get; set; }
    public Guid CandidateExamId { get; set; }
}
