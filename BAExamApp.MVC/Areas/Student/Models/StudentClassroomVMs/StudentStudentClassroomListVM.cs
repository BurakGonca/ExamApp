using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Student.Models.StudentClassroomVMs;

public class StudentStudentClassroomListVM
{
    public Guid ClassroomId { get; set; }

    [Display(Name = "Classroom_Name")]
    public string ClassroomName { get; set; }
    [Display(Name = "Opening_Date")]
    public DateTime OpeningDate { get; set; }
    [Display(Name = "Closed_Date")]
    public DateTime ClosedDate { get; set; }

    [Display(Name = "Branch_Name")]
    public string BranchName { get; set; }
}