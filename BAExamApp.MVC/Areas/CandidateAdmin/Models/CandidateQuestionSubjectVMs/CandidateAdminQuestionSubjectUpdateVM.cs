using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionSubjectVMs;

public class CandidateAdminQuestionSubjectUpdateVM
{
    public Guid Id { get; set; }

    [DisplayName("Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Konu adı en az 2 karakterden oluşmalıdır.")]
    public string Name { get; set; }
}
