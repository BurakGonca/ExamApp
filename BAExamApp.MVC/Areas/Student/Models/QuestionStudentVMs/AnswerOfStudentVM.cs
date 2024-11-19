namespace BAExamApp.MVC.Areas.Student.Models.QuestionStudentVMs;

public class AnswerOfStudentVM
{
    public Guid QuestionId { get; set; }
    public List<string>? Answers { get; set; }
    public Guid? ExamStudentId { get; set; }
    public IFormFile FileAnswer { get; set; }
    public bool? LikeDislikeStatus { get; set; }
    public int TimeSpent { get; set; }
}
