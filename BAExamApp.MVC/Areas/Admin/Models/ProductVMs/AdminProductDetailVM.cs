using BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ProductVMs;

public class AdminProductDetailVM
{
    public Guid Id { get; set; }

    [Display(Name = "Product")]
    public string Name { get; set; }

    [Display(Name = "IsActive")]
    public bool? IsActive { get; set; }

    [Display(Name = "TechnicalUnit_Name")]
    public string TechnicalUnitName { get; set; }

    [Display(Name = "Classroom")]
    public int ClassRoomCount { get; set; }

    [Display(Name = "Subject_List")]
    public List<AdminSubjectListVM> ProductSubjects { get; set; }

    [Display(Name = "Trainer_List")]
    public List<AdminProductTrainerVM> TrainerProducts { get; set; }
    public bool IsClassroomUsed { get; set; }
    public bool IsTrainerUsed { get; set; }
    public bool IsExamRuleUsed { get; set; }
}