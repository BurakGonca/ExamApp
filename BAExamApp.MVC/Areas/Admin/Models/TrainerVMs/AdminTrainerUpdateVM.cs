using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.TrainerVMs;

public class AdminTrainerUpdateVM
{
    public Guid Id { get; set; }

    [Display(Name = "First_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string FirstName { get; set; }

    [Display(Name = "Last_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string LastName { get; set; }

    [Display(Name = "Gender")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public bool? Gender { get; set; }

    //[Display(Name = "Date_Of_Birth")]
    //[Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    //public DateTime DateOfBirth { get; set; }

    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }
    public byte[]? OriginalImage { get; set; }
    public string? Image { get; set; }
    public bool RemoveImage { get; set; }

    [Display(Name = "TechnicalUnit_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public Guid TechnicalUnitId { get; set; }
    public SelectList? TechnicalUnitList { get; set; }

    [Display(Name = "Product")]
    public List<Guid>? ProductIds { get; set; }

    [Display(Name = "Product_List")]
    public SelectList? ProductList { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string Email { get; set; }
   
    [Display(Name = "OtherEmails")]
    public List<string>? OtherEmails { get; set; }

    //[Display(Name = "Talent_Names")]
    //[Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    //public List<Guid>? TrainerTalentIds { get; set; }

   // [Display(Name = "Talent_Names")]
    //public SelectList? TalentList { get; set; }
}