using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.DbSets.Candidates;
public class CandidateQuestionRule : AuditableEntity
{
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public int QuestionCount { get; set; }
    public Guid CandidateQuestionSubjectId { get; set; }    
    public Guid CandidateExamRuleId { get; set; }
    public virtual CandidateQuestionSubject CandidateQuestionSubject { get; set; }
    public virtual CandidateExamRule CandidateExamRule { get; set; }
    
}
