using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;

public class TrainerQuestionDetailsVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }
    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    [Display(Name = "State")]
    public State State { get; set; }
    [Display(Name = "Question_Type")]
    public QuestionType QuestionType { get; set; }
    [Display(Name = "Question_Target")]
    public string? Target { get; set; }
    [Display(Name = "Question_Gains")]
    public string? Gains { get; set; }
    [Display(Name = "Subtopic")]
    public List<string> SubtopicName { get; set; }
    [Display(Name = "Subject")]
    public List<string> SubjectName { get; set; }
    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }
    [Display(Name = "Time_Given")]
    public TimeSpan TimeGiven { get; set; }
    [Display(Name = "Review_Comment")]
    public string ReviewComment { get; set; }
    public string? RejectComment { get; set; }

    [Display(Name = "Question_Answers")]
    public List<QuestionAnswerDto> QuestionAnswers { get; set; }
    public string? Image { get; set; }
    public Dictionary<string, List<string>> CommonSubjectAndSubtopics { get; set; }
}