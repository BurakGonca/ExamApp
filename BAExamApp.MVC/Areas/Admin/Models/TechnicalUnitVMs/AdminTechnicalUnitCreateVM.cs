using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.TechnicalUnitVMs;

public class AdminTechnicalUnitCreateVM
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Teknik birim adı en az 2 karakterden oluşmalıdır.")]
    [MaxLength(256, ErrorMessage = "Teknik birim adı en fazla 500 karakterden oluşmalıdır.")]
    public string Name { get; set; }
}