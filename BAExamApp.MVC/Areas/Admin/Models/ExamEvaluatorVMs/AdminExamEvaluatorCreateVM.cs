using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamEvaluatorVMs;

public class AdminExamEvaluatorCreateVM
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Exam_Date")]
    public DateTime ExamDateTime { get; set; }

    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; }

    [Display(Name = "Exam_Rule")]
    public string ExamRuleName { get; set; }

    [Display(Name = "Classroom")]
    public string ClassroomName { get; set; }

    [Display(Name = "Exams_Evaluators")]
    public List<Guid> TrainerIds { get; set; }
}
