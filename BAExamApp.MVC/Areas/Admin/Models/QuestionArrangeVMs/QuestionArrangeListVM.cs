using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionArrangeVMs;

public class QuestionArrangeListVM
{
    public Guid Id { get; set; }

    [Display(Name = "QuestionArrange_Comment")]
    public string Comment { get; set; }

    [Display(Name = "QuestionArrange_CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Admin_Name")]
    public string AdminName { get; set; }
    public Guid ArrangerAdminId { get; set; }
}
