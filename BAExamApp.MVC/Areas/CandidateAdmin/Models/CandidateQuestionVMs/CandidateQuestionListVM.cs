using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionVMs;

public class CandidateQuestionListVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "Question_Type")]
    public int CandidateQuestionType { get; set; }

    [Display(Name = "Question_Subject")]
    public CandidateAdminQuestionSubjectDetailVM CandidateQuestionSubject { get; set; }

    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Modified_Date")]
    public DateTime ModifiedDate { get; set; }

    [Display(Name = "Modified_By")]
    public string ModifiedBy { get; set; }

    [Display(Name = "Status")]
    public Status Status { get; set; }
}
