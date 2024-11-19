using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExamInitiation;
public class CandidateExamAnswerQuestionDto
{
    public Guid CandidateQuestionId { get; set; }
    public Guid ExamId { get; set; }
    public Guid CandidateId { get; set; }
    public Guid? CandidateAnswerId { get; set; }
    public string? Answer { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }
}
