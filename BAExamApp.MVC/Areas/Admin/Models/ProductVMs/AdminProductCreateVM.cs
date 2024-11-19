using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ProductVMs;

public class AdminProductCreateVM
{
    [Display(Name = "Product_Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Eğitim adı en az 2 karakterden oluşmalıdır.")]
    [MaxLength(256, ErrorMessage = "Eğitim adı en fazla 256 karakterden oluşmalıdır.")]
    public string Name { get; set; }

    [Display(Name = "IsActive")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    public bool IsActive { get; set; } = true;
    [Display(Name = "TechnicalUnit_Id")]
    [Required(ErrorMessage = "Teknik birim seçmeden eğitim eklenemez.")]
    public Guid TechnicalUnitId { get; set; }
    [Display(Name = "TechnicalUnit_Id")]
    public SelectList? TechnicalUnitList { get; set; }

   

   
}