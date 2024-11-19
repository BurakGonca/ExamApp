using BAExamApp.Dtos.Trainers;
using BAExamApp.Entities.DbSets;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class AdminExamAddEvaluatorsVM
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Start_Time")]
    public TimeSpan StartTime { get; set; }

    [Display(Name = "Exam_Date")]
    public DateTime ExamDate { get; set; } = DateTime.Now;

    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; } = TimeSpan.FromHours(03.00);

    [Display(Name = "End_Time")]
    public string EndTime
    {
        get
        {
            var endTime = StartTime.Add(ExamDuration);
            return endTime.ToString("hh\\:mm\\:ss");
        }
    }

    [Display(Name = "Exam_Rule")]
    public ExamRule ExamRule { get; set; }

    [Display(Name = "Classroom")]
    public Classroom Classroom { get; set; }
  
    [Display(Name = "Exams_Evaluators")]
    public List<TrainerListDto> Trainers { get; set; }
}
