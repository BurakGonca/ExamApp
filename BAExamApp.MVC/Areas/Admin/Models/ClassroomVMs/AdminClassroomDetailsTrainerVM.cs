using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;

public class AdminClassroomDetailsTrainerVM
{
    public Guid Id { get; set; }

    [Display(Name = "First_Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last_Name")]
    public string LastName { get; set; }

    [Display(Name = "Trainer_Assigned_Date")]
    public DateTime AssignedDate { get; set; }

    [Display(Name = "Trainer_Resigned_Date")]
    public DateTime? ResignedDate { get; set; }

}
