using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExamRule;
public class CandidateExamRuleUpdateDto
{
    public Guid Id { get; set; }

    public ExamQuestionDistribution ExamQuestionDistribution { get; set; }
    public int TestQuestionsCoefficient { get; set; }
    public int ClassicQuestionsCoefficient { get; set; }
    public int AlgorithmQuestionsCoefficient { get; set; }

    public virtual ICollection<CandidateQuestionRuleDto> CandidateQuestionRules { get; set; }
}
