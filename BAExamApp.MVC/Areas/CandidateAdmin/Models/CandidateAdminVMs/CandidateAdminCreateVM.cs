using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAdminVMs;

public class CandidateAdminCreateVM
{
    [Display(Name = "First_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    [EmailAddress(ErrorMessage = "Lütfen geçerli bir mail adresi giriniz.")]
    public string Email { get; set; } = string.Empty;  
    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }
    public string Image { get; internal set; }
}
