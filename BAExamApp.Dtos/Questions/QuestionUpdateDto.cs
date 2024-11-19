using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.QuestionSubtopics;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.Questions;

public class QuestionUpdateDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public State State { get; set; }
    public int QuestionType { get; set; }
    public string? Image { get; set; }
    public string Target { get; set; }
    public string Gains { get; set; }
    public List<QuestionSubtopicsUpdateDto> SubtopicId { get; set; }
    public Guid SubjectId { get; set; }
    public Guid QuestionDifficultyId { get; set; }
    public List<QuestionAnswerUpdateDto> QuestionAnswers { get; set; }
}
