using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.AdminVMs;

public class AdminAdminCreateVM
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
    [Required(ErrorMessage = "Lütfen cinsiyet seçiniz.")]
    [Display(Name = "Gender")]
    public bool Gender { get; set; }
    //[Display(Name = "Date_Of_Birth")]
    //[Required(ErrorMessage = "{0} alanı boş bırakılamaz seçiniz.")]
    //[DataType(DataType.Date, ErrorMessage = "Lütfen geçerli bir tarih giriniz.")]
    //public DateTime DateOfBirth { get; set; }

    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }

    //[Display(Name = "City")]
    //[Required(ErrorMessage = "{0} alanı boş bırakılamaz seçiniz.")]


    [Display(Name = "OtherEmails")]
    public string? OtherEmails { get; set; }
}
