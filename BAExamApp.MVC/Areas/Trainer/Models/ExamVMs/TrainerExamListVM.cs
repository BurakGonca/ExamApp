using BAExamApp.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;

public class TrainerExamListVM
{
    public Guid Id { get; set; }

    [Display(Name ="Exam_Name")]
    public string Name { get; set; }

    [Display(Name = "Exam_Date")]
    public DateTime ExamDateTime { get; set; } = DateTime.Now;

    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; }

    [Display(Name = "Classroom")]
    public string ClassroomName { get; set; }
    [Display(Name = "Exam_Type")]
    public ExamType ExamType { get; set; }
    [DisplayName("Question_CreatedDate")]
    public DateTime CreatedDate { get; set; }
    public bool IsStarted { get; set; }
    

}