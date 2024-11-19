using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.QuestionRevisionVMs;

public class TrainerQuestionRevisionListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Question_ModifierName")]
    public string RequesterAdminName { get; set; } = null!;


    [Display(Name = "Question_RequestDate")]
    public DateTime CreatedDate { get; set; }


    [Display(Name = "Question_ModifierFullName")]
    public string RequestedTrainerName { get; set; } = null!;


    [Display(Name = "Question_CompletionDate")]
    public DateTime ModifiedDate { get; set; }


    [Display(Name = "Question_Request_Description")]
    public string? RequestDescription { get; set; }


    [Display(Name = "Question_Revision_Conclusion")]
    public string? RevisionConclusion { get; set; }


    public Guid QuestionId { get; set; }
}
