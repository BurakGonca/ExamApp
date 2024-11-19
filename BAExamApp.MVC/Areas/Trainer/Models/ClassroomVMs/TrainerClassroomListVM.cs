using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.ClassroomVMs;

public class TrainerClassroomListVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Classroom_Name")]
    public string Name { get; set; }
    [Display(Name = "Opening_Date")]
    public DateTime OpeningDate { get; set; }
    [Display(Name = "Closed_Date")]
    public DateTime ClosedDate { get; set; }

    [Display(Name = "Student_Count")]
    public int StudentCount { get; set; }
}