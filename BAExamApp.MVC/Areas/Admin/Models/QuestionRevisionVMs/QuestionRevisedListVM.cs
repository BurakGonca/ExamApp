using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionRevisionVMs;

public class QuestionRevisedListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "Question_CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Question_ModifiedDate")]
    public DateTime ModifiedDate { get; set; }

    [Display(Name = "Question_SubjectName")]
    public string SubjectName { get; set; }

    [Display(Name = "Question_IsApproved")]
    public State IsApproved { get; set; }

    public int RevisionCount { get; set; }
}
