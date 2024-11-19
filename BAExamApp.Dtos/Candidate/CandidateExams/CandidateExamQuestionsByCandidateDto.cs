using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExams;
public class CandidateExamQuestionsByCandidateDto
{
    public Guid CandidateId { get; set; }
    public Guid ExamId { get; set; }
    public string FullName { get; set; }
    public string ExamName { get; set; }
    public CandidateExamQuestionsByCandidateDto()
    {
        Questions=new HashSet<CandidateExamQuestionsByCandidateListDto>();
    }

    public ICollection<CandidateExamQuestionsByCandidateListDto> Questions { get; set; }
}
