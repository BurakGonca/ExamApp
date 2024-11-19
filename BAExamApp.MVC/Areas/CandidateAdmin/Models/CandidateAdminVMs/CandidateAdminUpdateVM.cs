using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAdminVMs;

public class CandidateAdminUpdateVM
{
    public Guid Id { get; set; }

    [Display(Name = "First_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string FirstName { get; set; }

    [Display(Name = "Last_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string LastName { get; set; }
    public string Image { get; set; }

    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string Email { get; set; }
    
}
