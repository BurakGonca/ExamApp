using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateBranchVMs;

public class CandidateBranchCreateVM
{
    [Display(Name = "Branch_Name")]
    [Required(ErrorMessage = "Error_Blank")]
    [MinLength(2, ErrorMessage = "Şube adı en az 2 karakterden oluşmalıdır.")]
    [MaxLength(256, ErrorMessage = "Şube adı en fazla 256 karakterden oluşmalıdır.")]
    public string Name { get; set; }
}
