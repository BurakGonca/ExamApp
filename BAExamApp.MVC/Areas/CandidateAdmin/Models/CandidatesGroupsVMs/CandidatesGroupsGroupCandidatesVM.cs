namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidatesGroupsVMs;

public class CandidatesGroupsGroupCandidatesVM
{
    public Guid GroupId { get; set; }

    public string? Name { get; set; }

    public CandidatesGroupsGroupCandidatesVM()
    {
        Candidates = new HashSet<CandidatesGroupsCandidateListVM>();
        CandidateList = new HashSet<CandidatesGroupsCandidateListVM>();
    }
    public ICollection<CandidatesGroupsCandidateListVM> Candidates { get; set; }
    public ICollection<CandidatesGroupsCandidateListVM> CandidateList { get; set; }
}
