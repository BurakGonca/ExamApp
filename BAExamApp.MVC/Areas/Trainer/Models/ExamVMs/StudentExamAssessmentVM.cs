using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;

public class StudentExamAssessmentVM
{
    public Guid StudentId { get; set; }
    public Guid ExamId { get; set; }

    [Display(Name = "Content")]
    public string Content { get; set; }
    public string TrainerName { get; set; }
}
