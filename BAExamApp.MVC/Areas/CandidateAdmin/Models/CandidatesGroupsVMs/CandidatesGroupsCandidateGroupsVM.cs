using BAExamApp.Dtos.Candidate.CandidatesGroups;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidatesGroupsVMs;

public class CandidatesGroupsCandidateGroupsVM
{
    public Guid CandidateId { get; set; }

    public string? FullName { get; set; }

    public CandidatesGroupsCandidateGroupsVM()
    {
        Groups = new HashSet<CandidatesGroupsGroupListVM>();
        GroupList = new HashSet<CandidatesGroupsGroupListVM>();
    }
    public ICollection<CandidatesGroupsGroupListVM> Groups { get; set; }
    public ICollection<CandidatesGroupsGroupListVM> GroupList { get; set; }
}
