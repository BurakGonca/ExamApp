using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.QuestionSubtopics;
using BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

public class AdminQuestionExamDetailsVM
{
    public Guid Id { get; set; }
    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    public int QuestionType { get; set; }
    [Display(Name = "Image")]
    public string Image { get; set; }
    public string Target { get; set; }
    public string Gains { get; set; }
    [Display(Name = "Subtopic")]
    public List<string> SubtopicName { get; set; }
    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }
    public List<AdminQuestionDetailsQuestionAnswerVM> QuestionAnswers { get; set; }
}
