using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

public class AdminQuestionUpdateVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    [Display(Name = "State")]
    public State State { get; set; }
    [Required(ErrorMessage = "zorunlu")]
    [Display(Name = "Question_Type")]
    public QuestionType QuestionType { get; set; }
    public List<SelectListItem>? QuestionTypeList { get; set; }

    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }
    public string? Image { get; set; }

    [Display(Name = "Question_Target")]
    public string Target { get; set; }
    [Display(Name = "Question_Gains")]
    public string Gains { get; set; }
    //[Display(Name = "Question_AnswersZip")]
    //public IFormFile? QuestionAnswersZip { get; set; }

    [Display(Name = "Subtopic")]
    public List<Guid> SubtopicId { get; set; }
    public List<SelectListItem>? SubtopicList { get; set; }
    [Display(Name = "Product")]
    public Guid ProductId { get; set; }
    public List<SelectListItem>? ProductList { get; set; }
    [Display(Name = "Subject")]
    public Guid SubjectId { get; set; }
    public List<SelectListItem>? SubjectList { get; set; }

    [Display(Name = "Question_Difficulty")]
    public Guid QuestionDifficultyId { get; set; }
    public SelectList QuestionDifficultyList { get; set; }
    [Display(Name = "Time_Given")]
    public TimeSpan TimeGiven { get; set; }

    [Display(Name = "Review_Comment")]
    public string? ReviewComment { get; set; }

    [Display(Name = "Review_Comment")]
    public string? ReviseComment { get; set; }
    [Display(Name = "Arrange_Comment")]
    public string? ArrangeComment { get; set; }
    [BindProperty]
    public List<TrainerQuestionAnswerUpdateVM> QuestionAnswers { get; set; }
    public string QuestionAnswersList { get; set; }
}
