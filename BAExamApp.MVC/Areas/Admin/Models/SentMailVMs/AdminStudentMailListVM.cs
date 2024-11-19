using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.SentMailVMs;

public class AdminStudentMailListVM
{

    public Guid Id { get; set; }
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Display(Name = "StudentFullName")]
    public string StudentFullName { get; set; }
    [Display(Name = "Trainers")]
    public string LatestClassroomsTrainers { get; set; }

    [Display(Name = "Latest_Classroom")]
    public string LatestClassroom { get; set; }

    [Display(Name = "Subject")]
    public string Subject { get; set; }
    [Display(Name = "Content")]
    public string Content { get; set; }
    [Display(Name = "IsSuccess")]
    public bool IsSuccess { get; set; }
    [Display(Name = "CreatedDate")]
    public DateTime CreatedDate { get; set; }
    [Display(Name = "ModifiedDate")]
    public DateTime? ModifiedDate { get; set; }
}
