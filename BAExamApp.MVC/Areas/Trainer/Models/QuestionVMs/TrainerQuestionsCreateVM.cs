using BAExamApp.Dtos.Subtopics;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;

public class TrainerQuestionsCreateVM
{
    public TrainerQuestionsCreateVM()
    {
        QuestionAnswers = new List<TrainerQuestionAnswerCreateVM>();
    }
    [Display(Name = "Question_Image")]
    [JsonIgnore]
    public IFormFile? Image { get; set; }
    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    [Display(Name = "Question_Type")]
    public string QuestionType { get; set; }

    [Display(Name = "Question_Target")]
    public string Target { get; set; }
    [Display(Name = "Question_Gains")]
    public string Gains { get; set; }

    [Display(Name = "Subtopic")]
    public Guid SubtopicId { get; set; }

    //public string SubtopicName { get; set; }
    //public SubtopicDto Subtopic { get; set; }
    [Display(Name = "Product")]
    public Guid ProductId { get; set; }
    
    //public string? ProductName { get; set; }
    [Display(Name = "Subject")]
    public Guid SubjectId { get; set; }
    
    //public string SubjectName { get; set; }

    [Display(Name = "Question_Difficulty")]
    public Guid QuestionDifficultyId { get; set; }
    //public string QuestionDifficultyName { get; set; }
    [Display(Name = "Time_Given")]
    public TimeSpan TimeGiven { get; set; }

    [BindProperty]
    public List<TrainerQuestionAnswerCreateVM> QuestionAnswers { get; set; }

    public class FileViewModel
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        // Diğer gerekli özellikleri ekleyebilirsiniz
    }
}
