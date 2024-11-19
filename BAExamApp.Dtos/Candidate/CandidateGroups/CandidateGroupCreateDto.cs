using BAExamApp.Dtos.ClassroomProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateGroups;
public class CandidateGroupCreateDto
{
    public string Name { get; set; }
    public Guid CandidateBranchId { get; set; }
}
