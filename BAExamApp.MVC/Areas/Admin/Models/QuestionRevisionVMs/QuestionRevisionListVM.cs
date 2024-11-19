using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionRevisionVMs;

public class QuestionRevisionListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Question_ModifierName")]
    public string RequesterAdminName { get; set; } = null!;


    [Display(Name = "Question_CreatedDate")]
    public DateTime CreatedDate { get; set; }


    [Display(Name = "Question_ModifierFullName")]
    public string RequestedTrainerName { get; set; } = null!;


    [Display(Name = "Question_ModifiedDate")]
    public DateTime ModifiedDate { get; set; }


    [Display(Name = "Question_Request_Description")]
    public string? RequestDescription { get; set; }


    [Display(Name = "Question_Revision_Conclusion")]
    public string? RevisionConclusion { get; set; }

    [Display(Name = "State")]
    public State State { get; set; }


    public Guid QuestionId { get; set; }
}
