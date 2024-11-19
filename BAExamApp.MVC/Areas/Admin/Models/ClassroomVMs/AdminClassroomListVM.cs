using BAExamApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;

public class AdminClassroomListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Opening_Date")]
    public DateTime OpeningDate { get; set; }

    [Display(Name = "Closed_Date")]
    public DateTime ClosedDate { get; set; }


    [Display(Name = "Branch_Name")]
    public string BranchName { get; set; }
    [Display(Name = "Status")]
    public Status Status { get; set; }
    public string TrainerNames { get; set; }

    [Display(Name = "Student_Count")]
    public int StudentCount { get; set; }
    public bool IsActive { get; set; }
    [Display(Name = "Conducted_Exam")]
    public int ClassroomExamCount { get; set; }
    [Display(Name = "Appointed_Exam")]
    public int ClassroomAppointedExamCount { get; set; }
}