using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;

public class AdminGroupTypeListVM
{
    public Guid Id { get; set; }
    [Display(Name = "Group_Type")]
    public string Name { get; set; }
    [Display(Name = "Information")]
    public string Information { get; set; }
}