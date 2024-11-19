using BAExamApp.Dtos.StudentQuestions;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Student.Models.StudentExamVMs;

public class StudentStudentExamReportVM
{
    [Display(Name = "Id")]
    public string StudentExamId { get; set; }
    [Display(Name = "Exam_Id")]
    public string ExamId { get; set; }
    [Display(Name = "Exam_Name")]
    public string ExamName { get; set; }
    [Display(Name = "Exam_Date_Time")]
    public DateTime ExamDateTime { get; set; }
    [Display(Name = "Exam_Duration")]
    public TimeSpan ExamDuration { get; set; }
    [Display(Name = "Exam_Duration")]
    public string? FormattedExamDuration { get; set; }
    [Display(Name = "Question_Count")]
    public int QuestionCount { get; set; }

    [Display(Name = "Student_Name")]
    public string StudentFullname { get; set; }
    [Display(Name = "Max_Score")]
    public decimal MaxScore { get; set; }
    [Display(Name = "Score")]
    public decimal Score { get; set; }

    [Display(Name = "Total_Time_Spend")]
    public TimeSpan TotalTimeSpend { get; set; }

    [Display(Name = "Total_Time_Spend")]
    public string FormattedTotalTimeSpend { get; set; }

    [Display(Name = "Subtopic_Performance")]
    public Dictionary<string, double> SubtopicPerformances { get; set; }

    [Display(Name = "Right_Answer")]
    public Dictionary<string, int> SubtopicRightAnswers { get; set; }
    [Display(Name = "Wrong_Answer")]
    public Dictionary<string, int> SubtopicWrongAnswers { get; set; }
    [Display(Name = "Empty_Answer")]
    public Dictionary<string, int> SubtopicEmptyAnswers { get; set; }

    public List<StudentQuestionDto> StudentQuestions { get; set; }
}
