using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.TrainerVMs;

public class TrainerTrainerUpdateVM
{
    public Guid Id { get; set; }

    [Display(Name = "First_Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last_Name")]
    public string LastName { get; set; }

    [Display(Name = "Gender")]
    public bool Gender { get; set; }

    //[Display(Name = "Date_Of_Birth")]
    //public DateTime DateOfBirth { get; set; }

    [Display(Name = "Profile_Image")]
    public IFormFile? ImageFile { get; set; }

    public string Image { get; set; }
}