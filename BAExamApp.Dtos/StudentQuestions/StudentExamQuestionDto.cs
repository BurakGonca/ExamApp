using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.StudentAnswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.StudentQuestions;
public class StudentExamQuestionDto
{
    public int MaxScore { get; set; }
    public int BonusScore { get; set; }
    public int? Score { get; set; }
    public int QuestionOrder { get; set; }
    public DateTime? TimeStarted { get; set; }
    public DateTime? TimeFinished { get; set; }
    public Guid? StudentExamId { get; set; }
    public Guid? QuestionId { get; set; }
    public QuestionDetailForAdminDto Question { get; set; }
    public List<StudentAnswerDetailDto> StudentAnswers { get; set; }
}
