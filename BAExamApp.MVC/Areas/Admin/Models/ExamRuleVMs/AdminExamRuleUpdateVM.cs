using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Admin.Models.ExamRuleSubtopicVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamRuleVMs;

public class AdminExamRuleUpdateVM
{
    public Guid Id { get; set; }

    [Display(Name = "Exam_Rule_Name")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public string Name { get; set; }

    [Display(Name = "Exam_Rule_Description")]
    public string? Description { get; set; }

    [Display(Name = "Product")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public Guid ProductId { get; set; }

    [BindProperty]
    public List<AdminExamRuleSubtopicUpdateVM>? ExamRuleSubtopics { get; set; }
}