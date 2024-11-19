using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidatesGroups;
public class CandidatesGroupsGroupCandidatesDto
{
    public Guid GroupId { get; set; }

    public string? Name { get; set; }

    public CandidatesGroupsGroupCandidatesDto()
    {
        Candidates = new HashSet<CandidatesGroupsCandidateListDto>();
        CandidateList = new HashSet<CandidatesGroupsCandidateListDto>();
    }
    public ICollection<CandidatesGroupsCandidateListDto> Candidates { get; set; }
    public ICollection<CandidatesGroupsCandidateListDto> CandidateList { get; set; }
}
