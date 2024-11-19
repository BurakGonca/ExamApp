using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateGroups;
public class CandidateGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CandidateBranchId { get; set; }
}
