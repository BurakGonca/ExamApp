using BAExamApp.Dtos.Candidate.CandidateExams;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamQuestionsByCandidateListVM
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public byte[]? Image { get; set; }
    public int? Order { get; set; }
    public int MaxScore { get; set; }
    public int? Score { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }



    public List<CandidateExamQuestionsByCandidateAnswersListVM> Answers { get; set; }
}
