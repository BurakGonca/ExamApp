namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionVMs;

public class CandidateQuestionIndexVM
{
    public List<CandidateQuestionListVM>? QuestionList { get; set; }
    public string? Content { get; set; }
    public List<SelectListItem> CandidateQuestionSubjectList { get; set; }
    public List<SelectListItem> CandidateQuestionTypeList { get; set; }
    public int CandidateQuestionTypeIndex { get; set; }
    public Guid CandidateQuestionSubjectIndex { get; set; }
    public bool ShowPassiveQuestions { get; set; }
}
