using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.QuestionSubtopics;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Questions;
public class QuestionDetailForAdminDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int QuestionType { get; set; }
    public string? Image { get; set; }
    public string Target { get; set; }
    public string Gains { get; set; }
    public List<string> SubtopicName { get; set; }
    public string QuestionDifficultyName { get; set; }
    public List<QuestionAnswerDto> QuestionAnswers { get; set; }
}
