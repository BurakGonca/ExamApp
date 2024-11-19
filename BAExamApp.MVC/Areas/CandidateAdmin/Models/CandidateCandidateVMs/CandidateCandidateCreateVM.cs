using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateCandidateVMs;

public class CandidateCandidateCreateVM
{
    [Display(Name = "First_Name")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last_Name")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; } = null!;
}
