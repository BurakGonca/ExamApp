using BAExamApp.Dtos.QuestionAnswers;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.Dtos.Questions;

public class QuestionDetailsDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Content { get; set; }
    public int State { get; set; }
    public int QuestionType { get; set; }
    public string Target { get; set; }
    public string Gains { get; set; }
    public string? RejectComment { get; set; }
    public List<string> SubtopicName { get; set; }
    public List<string> SubjectName { get; set; }
    public string QuestionDifficultyName { get; set; }
    public string? Image { get; set; }
    public TimeSpan TimeGiven { get; set; }
    public List<QuestionAnswerDto> QuestionAnswers { get; set; }
}