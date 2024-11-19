using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidatesExams;
public class CandidatesExamsDto
{
    public Guid CandidateId { get; set; }
    public Guid CandidateExamId { get; set;}
    public DateTime? StartDate { get; set; }
}
