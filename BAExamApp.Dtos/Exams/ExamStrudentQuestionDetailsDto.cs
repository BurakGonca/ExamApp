using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.StudentQuestions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Exams;
public class ExamStrudentQuestionDetailsDto
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

    public ICollection<StudentExamQuestionDto> StudentQuestions { get; set; }
}
