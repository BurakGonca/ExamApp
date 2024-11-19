using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.Subtopics;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;

public class AdminSubjectDetailVM
{
    public Guid Id { get; set; }
    [Display(Name ="Subject")]
    public string Name { get; set; }
    [Display(Name = "Products")]
    public List<ProductListDto> Products { get; set; }
    public List<SubtopicListDto> Subtopics { get; set; }

    public bool IsQuestionUsed {  get; set; }
    public bool IsSubtopicUsed { get; set; }
    public bool IsProductUsed { get; set; }
    public Status Status { get; set; }

}
