using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

public class AdminQuestionDetailsVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }
    [Display(Name = "Created_By")]
    public string CreatedBy { get; set; }
    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }
    [Display(Name = "Modified_Date")]
    public DateTime ModifiedDate { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    [Display(Name = "State")]
    public State State { get; set; }
    [Display(Name = "Question_Type")]
    public QuestionType QuestionType { get; set; }
    [Display(Name = "Image")]
    public string Image { get; set; }

    [Display(Name = "Subtopic")]
    public List<string> SubtopicName { get; set; }
    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }

    [Display(Name = "Review_Comment")]
    public string ReviewComment { get; set; }

    [Display(Name = "Question_Answers")]
    public List<QuestionAnswerDto> QuestionAnswers { get; set; }
}