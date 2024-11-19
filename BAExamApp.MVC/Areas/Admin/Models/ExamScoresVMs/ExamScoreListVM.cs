using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamScoresVMs;

public class ExamScoreListVM
{
    public Guid Id { get; set; }

    [DisplayName("Name")]
    public string Name { get; set; }

    [DisplayName("Sınıf Adı")]
    public string ClassroomName { get; set; }

    [Display(Name = "Exam_Date")]
    public DateTime ExamDateTime { get; set; }

    [Display(Name = "Puan")]
    public string Score { get; set; }

    [DisplayName("Eğitmen")]
    public string Evaluator { get; set; }
}
