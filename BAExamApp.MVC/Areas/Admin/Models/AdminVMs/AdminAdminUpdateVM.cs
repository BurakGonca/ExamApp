using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.AdminVMs;

public class AdminAdminUpdateVM
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
    public byte[]? OriginalImage { get; set; } //Detaylarda fotoyu görüntülerken db deki fotoyu bu prop aracılığıyla alacak
    public bool RemoveImage { get; set; }

    //[Display(Name = "Date_Of_Birth")]
    //[Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    //[DataType(DataType.Date, ErrorMessage = "Lütfen geçerli bir tarih giriniz.")]
    //public DateTime DateOfBirth { get; set; }

    [Display(Name = "Gender")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public bool Gender { get; set; }

    //[Display(Name = "City")]
    //[Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string Email { get; set; }
    [Display(Name = "OtherEmails")]
    public List<string>? OtherEmails { get; set; }
    public List<UserRoleAssingDto> SelectedRoleList { get; set; }
    public List<string> Roles { get; set; }
}