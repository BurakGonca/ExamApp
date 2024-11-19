using BAExamApp.Dtos.ExamRuleSubtopics;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.ExamRules;

public class ExamRuleUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ExamCreationType ExamCreationType { get; set; }
    public Guid ProductId { get; set; }
    public List<ExamRuleSubtopicUpdateDto> ExamRuleSubtopics { get; set; }
}