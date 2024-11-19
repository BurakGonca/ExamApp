using BAExamApp.Entities.DbSets;
using Microsoft.Identity.Client;

namespace BAExamApp.MVC.Areas.Student.Models.QuestionStudentVMs;

public class QuestionForStudentListVM
{
    public List<Question> RandomQuestionList { get; set; } = new();
    public List<StudentQuestion> QuestionStudents { get; set; } = new();
    public List<QuestionAnswer> QuestionAnswers { get; set; } = new();

    public Guid? NextQuestionId { get; set; }
    public Guid? QuestionId { get; set; }
    public List<string>? Answers { get; set; }
    public string? ExamName { get; set; }
    public IFormFile? Documentation { get; set; }
    public Guid? ExamStudentId { get; set; }

}
