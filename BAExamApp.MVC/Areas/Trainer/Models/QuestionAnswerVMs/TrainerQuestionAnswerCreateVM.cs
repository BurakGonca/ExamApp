namespace BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;

public class TrainerQuestionAnswerCreateVM
{
    public Guid Id { get; set; }
    public string Answer { get; set; }
    public bool IsRightAnswer { get; set; }
    public bool IsAnswerImage { get; set; }
    public Guid QuestionId { get; set; }
}