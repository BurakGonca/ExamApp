using System;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs
{
    public class AdminQuestionAnswerCreateVM
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsRightAnswer { get; set; }
        public bool IsAnswerImage { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? SubtopicId { get; set; }
    }
}
