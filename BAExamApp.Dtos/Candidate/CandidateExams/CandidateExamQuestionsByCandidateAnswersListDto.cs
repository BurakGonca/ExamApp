using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExams;
public class CandidateExamQuestionsByCandidateAnswersListDto
{
    public Guid Id { get; set; }
    public string Answer { get; set; }
    public bool IsImageAnswer { get; set; }
    public bool IsRightAnswer { get; set; }


}
