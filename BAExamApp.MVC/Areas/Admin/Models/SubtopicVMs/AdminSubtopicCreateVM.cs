using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.SubtopicVMs;

public class AdminSubtopicCreateVm
{
    [Display(Name = "Subtopic_Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Altkonu adı en az 2 karakterden oluşmalıdır.")]
    [MaxLength(256, ErrorMessage = "Altkonu Adı en fazla 256 karakterden oluşmalıdır.")]
    public string Name { get; set; }

    [Display(Name = "IsActive")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    public bool IsActive { get; set; }

    [Display(Name = "Subject_Name")]
    [Required(ErrorMessage = "Konu seçmeden Altkonu eklenemez.")]
    public Guid SubjectId { get; set; }

    [Display(Name = "Subject_List")]
    public SelectList? SubjectList { get; set; }
}
