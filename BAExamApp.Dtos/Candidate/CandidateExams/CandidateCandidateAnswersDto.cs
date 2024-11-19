using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExams;
public class CandidateCandidateAnswersDto
{
    public Guid Id { get; set; }
    public string? Answer { get; set; }
    public bool? IsRightAnswer { get; set; }
    public string? AIAssessment { get; set; }

    // NAV PROP
    public Guid CandidateQuestionId { get; set; }
    public Guid? CandidateAnswerId { get; set; }
}
