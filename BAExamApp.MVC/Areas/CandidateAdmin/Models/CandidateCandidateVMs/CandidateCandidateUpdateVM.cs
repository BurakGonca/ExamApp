using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateCandidateVMs;

public class CandidateCandidateUpdateVM
{
    public Guid Id { get; set; }

    [Display(Name = "First_Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last_Name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }

    [Display(Name = "Profile_Image")]
    public byte[]? Image { get; set; }
}
