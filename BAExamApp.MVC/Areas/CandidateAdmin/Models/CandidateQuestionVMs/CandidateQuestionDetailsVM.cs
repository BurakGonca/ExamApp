using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
using BAExamApp.Dtos.QuestionAnswers;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionVMs;

public class CandidateQuestionDetailsVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "Question_Type")]
    public CandidateQuestionType CandidateQuestionType { get; set; }

    public string CandidateQuestionTypeString { get; set; }

    [Display(Name = "Question_Image")]
    public byte[]? Image { get; set; } 

    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Question_Answers")]
    public List<CandidateAnswerDto> QuestionAnswers { get; set; }


    [Display(Name = "Question_Subject")]
    public CandidateQuestionSubjectDto CandidateQuestionSubject { get; set; }
    public string CandidateQuestionSubjectString { get; set; }




}
