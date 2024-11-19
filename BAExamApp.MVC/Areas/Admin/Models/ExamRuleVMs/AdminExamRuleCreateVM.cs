using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Admin.Models.ExamRuleSubtopicVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamRuleVMs;

public class AdminExamRuleCreateVM
{
    [Display(Name = "Exam_Rule_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string Name { get; set; }

    [Display(Name = "Exam_Rule_Description")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string? Description { get; set; }

    [Display(Name = "Product")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public Guid ProductId { get; set; }

    [BindProperty]
    public List<AdminExamRuleSubtopicCreateVM>? ExamRuleSubtopics { get; set; }
}