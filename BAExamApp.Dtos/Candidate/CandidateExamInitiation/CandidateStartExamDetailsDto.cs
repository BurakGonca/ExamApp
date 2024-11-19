using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExamInitiation;
public class CandidateStartExamDetailsDto
{
    public Guid ExamId { get; set; }
    public Guid CandidateId { get; set; }
    public string ExamName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public bool IsExamStarted { get; set; }
    public bool IsExamFinished { get; set; }
    public int QuestionCount { get; set; }
}
