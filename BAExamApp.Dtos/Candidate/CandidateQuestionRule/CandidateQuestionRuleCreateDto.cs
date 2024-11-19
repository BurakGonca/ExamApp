using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateQuestionRule;
public class CandidateQuestionRuleCreateDto
{
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public int QuestionCount { get; set; }
    public Guid CandidateQuestionSubjectId { get; set; }
    public Guid CandidateExamRuleId { get; set; }
}
