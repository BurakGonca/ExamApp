using BAExamApp.Entities.DbSets;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionRevisionVMs;

public class QuestionRevisionCreateVM
{
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? RequestDescription { get; set; }
    public virtual Question? Question { get; set; }
    public Guid QuestionId { get; set; }
}
