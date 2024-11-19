using BAExamApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ProductVMs;

public class AdminProductUpdateVM
{
    public Guid Id { get; set; }

    [Display(Name = "Product_Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Eğitim adı en az 2 karakterden oluşmalıdır.")]
    [MaxLength(256, ErrorMessage = "Eğitim adı en fazla 256 karakterden oluşmalıdır.")]
    public string Name { get; set; }

    [Display(Name = "IsActive")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    public bool IsActive { get; set; }

    [Display(Name = "TechnicalUnit_Id")]
    [Required(ErrorMessage = "Teknik birim seçmeden eğitim eklenemez.")]
    public Guid TechnicalUnitId { get; set; }

    [Display(Name = "Subject_List")]
    [Required(ErrorMessage = "Konu seçmeden eğitim eklenemez.")]
    public List<Guid>? SubjectIds { get; set; }
    public SelectList? SubjectList { get; set; }
    public SelectList? TechnicalUnitList { get; set; }
    public Status Status { get; set; }
}
