using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;
using BAExamApp.MVC.Areas.Admin.Models.StudentQuestion;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ExamVMs;

public class AdminExamStudentQuestionDetailsVM
{
    public Guid ExamId { get; set; }
    [Display(Name = "Student_Name")]
    public string StudentName { get; set; }
    [Display(Name = "Exam_Name")]
    public string ExamName { get; set; }
    [Display(Name = "Exam_Score")]
    public decimal? Score { get; set; }

    [Display(Name = "Max_Score")]
    public decimal MaxScore { get; set; }

    [Display(Name = "Classroom")]
    public List<string> ClassroomNames { get; set; }

    public List<AdminStudentExamQuestionVm> StudentQuestions { get; set; }
}
