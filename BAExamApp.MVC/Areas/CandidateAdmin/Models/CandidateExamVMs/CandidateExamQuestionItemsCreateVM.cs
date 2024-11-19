namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamQuestionItemsCreateVM
{
    public CandidateExamCreateVM CandidateExamCreateVM { get; set; }
    public List<CandidateExamQuestionItemsVM> CandidateExamQuestionItemsVM { get; set; }
}
