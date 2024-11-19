using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;

public class TrainerQuestionCreateVM
{
    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    [Display(Name = "Question_Type")]
    public QuestionType QuestionType { get; set; }
    [Display(Name = "Question_Image")]
    public IFormFile? Image { get; set; }
    [Display(Name = "Question_Target")]
    public string Target { get; set; }
    [Display(Name = "Question_Gains")]
    public string Gains { get; set; }
    //[Display(Name = "Question_AnswersZip")]
    //public IFormFile? QuestionAnswersZip { get; set; }

    [Display(Name = "Subtopic")]
    public List<Guid> SubtopicId { get; set; }
    [Display(Name = "Product")]
    public Guid ProductId { get; set; }
    [Display(Name = "Subject")]
    public Guid SubjectId { get; set; }

    [Display(Name = "Question_Difficulty")]
    public Guid QuestionDifficultyId { get; set; }
    [Display(Name = "Time_Given")]
    public TimeSpan TimeGiven { get; set; }

    [BindProperty]
    public List<TrainerQuestionAnswerCreateVM> QuestionAnswers { get; set; }
}