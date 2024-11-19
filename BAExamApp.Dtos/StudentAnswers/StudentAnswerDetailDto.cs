using BAExamApp.Dtos.QuestionAnswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.StudentAnswers;
public class StudentAnswerDetailDto
{
    public Guid Id { get; set; }
    public bool IsSelected { get; set; }
    public bool IsCorrect { get; set; }
    public Guid QuestionAnswerId { get; set; }
    public QuestionAnswerDto QuestionAnswer { get; set; }
}
