using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.DbSets.Candidates;
public class CandidateExamRule : AuditableEntity
{

    public CandidateExamRule()
    {
        CandidateQuestionRules = new HashSet<CandidateQuestionRule>();
    }

    public ExamQuestionDistribution ExamQuestionDistribution { get; set; }
    public virtual ICollection<CandidateQuestionRule> CandidateQuestionRules { get; set; }

    public int TestQuestionsCoefficient { get; set; }
    public int ClassicQuestionsCoefficient { get; set; }
    public int AlgorithmQuestionsCoefficient { get; set; }
    public Guid CandidateExamId { get; set; }
    public virtual CandidateExam Exam { get; set; }
}
