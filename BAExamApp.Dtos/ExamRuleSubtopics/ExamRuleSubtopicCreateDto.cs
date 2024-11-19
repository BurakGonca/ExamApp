namespace BAExamApp.Dtos.ExamRuleSubtopics;

public class ExamRuleSubtopicCreateDto
{
    public int ExamType { get; set; }
    public int QuestionType { get; set; }
    public int QuestionCount { get; set; }
    public Guid QuestionDifficultyId { get; set; }
    public Guid ExamRuleId { get; set; }
    public Guid SubtopicId { get; set; }
}