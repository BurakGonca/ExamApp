using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.StudentClassroomVMs;

public class TrainerStudentClassroomListForClassroomDetailsVM
{
    [Display(Name = "Student_Id")]
    public Guid StudentId { get; set; }
    [Display(Name = "Student_Name")]
    public string StudentFullName { get; set; }
}
