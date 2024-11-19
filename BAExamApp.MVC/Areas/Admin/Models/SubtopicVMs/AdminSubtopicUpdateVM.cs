using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.SubtopicVMs;

public class AdminSubtopicUpdateVM
{
    public Guid Id { get; set; }

    [Display(Name = "Subtopic_Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Eğitim adı en az 2 karakterden oluşmalıdır.")]
    [MaxLength(256, ErrorMessage = "Eğitim adı en fazla 256 karakterden oluşmalıdır.")]
    public string Name { get; set; }

    [Display(Name = "IsActive")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    public bool IsActive { get; set; }

    [Display(Name = "Subject_List")]
    [Required(ErrorMessage = "Konu seçmeden Altkonu eklenemez.")]
    public Guid? SubjectId { get; set; }
}
