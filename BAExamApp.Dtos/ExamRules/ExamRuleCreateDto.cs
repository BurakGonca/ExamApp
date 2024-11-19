using BAExamApp.Dtos.ExamRuleSubtopics;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.ExamRules;

public class ExamRuleCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProductId { get; set; }
    public List<ExamRuleSubtopicCreateDto> ExamRuleSubtopics { get; set; }
}