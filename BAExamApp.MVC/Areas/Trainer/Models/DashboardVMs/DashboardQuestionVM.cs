using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.DashboardVMs;

public class DashboardQuestionVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }

    [Display(Name = "State")]
    public State State { get; set; }

    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }
}
