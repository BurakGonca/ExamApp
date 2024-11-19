using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;

public class TrainerQuestionListVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    [Display(Name = "Question_Type")]
    public QuestionType QuestionType { get; set; }
    [Display(Name = "Subject")]
    public string SubjectName { get; set; }

    [Display(Name = "Subtopic")]
    public List<string> SubtopicName { get; set; }
    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }
    [Display(Name = "Time_Given")]
    public TimeSpan? TimeGiven { get; set; }

    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }
    [Display(Name = "Modified_Date")]
    public DateTime ModifiedDate { get; set; }
    [Display(Name = "Modified_By")]
    public string ModifiedBy { get; set; }

    [Display(Name = "Request_Description")]
    public string RequestDescription { get; set; }
}