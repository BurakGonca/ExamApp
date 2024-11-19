using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.TechnicalUnitVMs;

public class AdminTechnicalUnitUpdateVM
{
    public Guid Id { get; set; }
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    public string Name { get; set; }
}