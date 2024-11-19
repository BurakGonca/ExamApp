namespace BAExamApp.Dtos.ExamRuleSubtopics;

public class ExamRuleSubtopicUpdateDto
{
    public Guid Id { get; set; }
    public int ExamType { get; set; }
    public int QuestionType { get; set; }
    public int QuestionCount { get; set; }
    public Guid QuestionDifficultyId { get; set; }
    public Guid ExamRuleId { get; set; }
    public Guid SubtopicId { get; set; }
    public Guid SubjectId { get; set; }
}
