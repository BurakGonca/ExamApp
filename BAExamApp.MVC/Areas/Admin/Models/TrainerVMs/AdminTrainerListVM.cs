using BAExamApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.TrainerVMs;

public class AdminTrainerListVM
{
    public Guid Id { get; set; }
    [Display(Name = "FirstName")]
    public string FirstName { get; set; }
    [Display(Name = "LastName")]
    public string LastName { get; set; }
    [Display(Name = "Email")]
    public string Email { get; set; }
    public byte[]? NewImage { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Status Status { get; set; }
    public int ClassroomCount { get; set; }
}