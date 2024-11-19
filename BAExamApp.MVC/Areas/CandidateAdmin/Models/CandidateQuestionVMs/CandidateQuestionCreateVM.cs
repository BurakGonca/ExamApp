using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionAnswerVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionVMs;

public class CandidateQuestionCreateVM
{
    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "Question_Type")]
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public IEnumerable<SelectListItem> CandidateQuestionTypeList { get; set; }

    [Display(Name = "Question_Image")]
    public IFormFile? NewImage { get; set; }

    [BindProperty]
    public List<CandidateAnswerCreateVM> QuestionAnswers { get; set; }
    public Guid? CandidateQuestionSubjectId { get; set; }

    public string QuestionAnswersList { get; set; }

}
