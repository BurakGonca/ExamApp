using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Student.Models.ClassroomVMs;

public class StudentClassroomDetailsVM
{
    [Display(Name = "Classroom_Name")]
    public string Name { get; set; }

    [Display(Name = "Opening_Date")]
    public DateTime OpeningDate { get; set; }

    [Display(Name = "Closed_Date")]
    public DateTime ClosedDate { get; set; }

    [Display(Name = "Group_Type")]
    public string GroupTypeName { get; set; }

    [Display(Name = "Branch_Name")]
    public string BranchName { get; set; }

    [Display(Name = "Product_List")]
    public List<string> ProductNames { get; set; }

    [Display(Name = "Trainer_List")]
    public List<string> TrainerNames { get; set; }
}