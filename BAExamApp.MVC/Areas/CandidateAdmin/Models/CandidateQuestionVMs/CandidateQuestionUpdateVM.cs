using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionAnswerVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionVMs;

public class CandidateQuestionUpdateVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "Question_Type")]
    public CandidateQuestionType CandidateQuestionType { get; set; }


    [Display(Name = "Question_Subject")]
    public Guid CandidateQuestionSubjectId { get; set; }


    public List<SelectListItem>? CandidateQuestionTypeList { get; set; }


    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }
    public byte[]? Image { get; set; }

    [BindProperty]
    public List<CandidateAnswerUpdateVM> QuestionAnswers { get; set; }
    public string? QuestionAnswersList { get; set; }
    public string? CandidateQuestionSubjectList { get; set; }
}
