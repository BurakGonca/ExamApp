using BAExamApp.Dtos.StudentQuestions;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class ClassroomStudentPerformanceVM
{
    public ClassroomStudentPerformanceVM()
    {
        SubtopicPerformances = new Dictionary<string, double>();
    }
    public Guid ClassroomId { get; set; }
    public Dictionary<string, double> SubtopicPerformances { get; set; }
}

