//using BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;
//using BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;
//using System.ComponentModel.DataAnnotations;

//namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

//public class AdminQuestionCreateVM
//{

//    //[Display(Name = "Question_Content")]
//    //public string Content { get; set; }
//    //[Display(Name = "Question_Type")]
//    //public QuestionType QuestionType { get; set; }
//    //[Display(Name = "Question_Image")]
//    //public IFormFile? Image { get; set; }
//    //[Display(Name = "Question_Target")]
//    //public string Target { get; set; }
//    //[Display(Name = "Question_Gains")]
//    //public string Gains { get; set; }
//    ////[Display(Name = "Question_AnswersZip")]
//    ////public IFormFile? QuestionAnswersZip { get; set; }

//    //[Display(Name = "Subtopic")]
//    //public List<Guid> SubtopicId { get; set; }
//    //[Display(Name = "Product")]
//    //public Guid ProductId { get; set; }
//    //[Display(Name = "Subject")]
//    //public Guid SubjectId { get; set; }

//    //[Display(Name = "Question_Difficulty")]
//    //public Guid QuestionDifficultyId { get; set; }
//    //[Display(Name = "Time_Given")]
//    //public TimeSpan TimeGiven { get; set; }

//    //[BindProperty]
//    //public List<AdminQuestionDetailsQuestionAnswerVM> QuestionAnswers { get; set; }

//    //public class FileViewModel
//    //{
//    //    public string Name { get; set; }
//    //    public long Size { get; set; }
//    //    public string Type { get; set; }
//    //    // Diğer gerekli özellikleri ekleyebilirsiniz
//    //}
//    [Display(Name = "Question_Content")]
//    public string Content { get; set; }
//    [Display(Name = "Question_Type")]
//    public QuestionType QuestionType { get; set; }
//    [Display(Name = "Question_Image")]
//    public IFormFile? Image { get; set; }
//    [Display(Name = "Question_Target")]
//    public string Target { get; set; }
//    [Display(Name = "Question_Gains")]
//    public string Gains { get; set; }
//    //[Display(Name = "Question_AnswersZip")]
//    //public IFormFile? QuestionAnswersZip { get; set; }

//    [Display(Name = "Subtopic")]
//    public List<Guid> SubtopicId { get; set; }
//    [Display(Name = "Product")]
//    public Guid ProductId { get; set; }
//    [Display(Name = "Subject")]
//    public Guid SubjectId { get; set; }

//    [Display(Name = "Question_Difficulty")]
//    public Guid QuestionDifficultyId { get; set; }
//    [Display(Name = "Time_Given")]
//    public TimeSpan TimeGiven { get; set; }

//    [BindProperty]
//    public List<AdminQuestionCreateVM> QuestionAnswers { get; set; }








//}


using BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs
{
    public class AdminQuestionCreateVM
    {
        [Required]
        [Display(Name = "Question_Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Question_Type")]
        public QuestionType QuestionType { get; set; }

        [Display(Name = "Question_Image")]
        public IFormFile? Image { get; set; }

        [Required]
        [Display(Name = "Question_Target")]
        public string Target { get; set; }

        [Required]
        [Display(Name = "Question_Gains")]
        public string Gains { get; set; }

        [Required]
        [Display(Name = "Subtopic")]
        public List<Guid> SubtopicId { get; set; }

        [Required]
        [Display(Name = "Product")]
        public Guid ProductId { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public Guid SubjectId { get; set; }

        [Required]
        [Display(Name = "Question_Difficulty")]
        public Guid QuestionDifficultyId { get; set; }

        [Required]
        [Display(Name = "Time_Given")]
        public TimeSpan TimeGiven { get; set; }

        [BindProperty]
        public List<AdminQuestionAnswerCreateVM> QuestionAnswers { get; set; }
    }
}

