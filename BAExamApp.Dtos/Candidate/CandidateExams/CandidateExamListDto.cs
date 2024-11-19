using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExams;
public class CandidateExamListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public bool IsStarted { get; set; } = false;

    public DateTime? ExamLinkEndDate { get; set; }
}
