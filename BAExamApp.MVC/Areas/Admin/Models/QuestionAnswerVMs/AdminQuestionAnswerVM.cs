namespace BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;

public class AdminQuestionAnswerVM
{

   
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsRightAnswer { get; set; }
        public bool IsAnswerImage { get; set; }
        public Guid QuestionId { get; set; }
    
}
