namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamDetailsListVM
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Score { get; set; }

}
