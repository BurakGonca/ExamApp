using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.BranchVMs;

public class AdminBranchCreateVM
{
    [Display(Name = "Branch_Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Şube adı en az 2 karakterden oluşmalıdır.")]
    [MaxLength(256, ErrorMessage = "Şube adı en fazla 256 karakterden oluşmalıdır.")]
    public string Name { get; set; }
}
