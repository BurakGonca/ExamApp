using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.Candidates;
public class CandidateExamsDto
{
    public Guid CandidateId { get; set; }
    public string CandidateName { get; set; }
    public List<CandidateExamResultDto> Exams { get; set; }
}
public class CandidateExamResultDto
{
    public Guid ExamId { get; set; }
    public string ExamName { get; set; }
    public bool IsExamStarted { get; set; }
    public bool IsExamFinished { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public int? Score { get; set; }
    public int MaxScore { get; set; }
}
