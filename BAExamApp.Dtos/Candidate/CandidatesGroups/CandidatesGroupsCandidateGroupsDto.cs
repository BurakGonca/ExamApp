using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidatesGroups;
public class CandidatesGroupsCandidateGroupsDto
{
    public Guid CandidateId { get; set; }

    public string? FullName { get; set; }

    public CandidatesGroupsCandidateGroupsDto()
    {
        Groups=new HashSet<CandidatesGroupsGroupListDto>();
        GroupList=new HashSet<CandidatesGroupsGroupListDto>();
    }
    public ICollection<CandidatesGroupsGroupListDto> Groups { get; set; }
    public ICollection<CandidatesGroupsGroupListDto> GroupList { get; set; }

}
