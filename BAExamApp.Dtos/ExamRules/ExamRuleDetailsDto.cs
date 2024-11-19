using BAExamApp.Core.Enums;
using BAExamApp.Dtos.ExamRuleSubtopics;
using BAExamApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.Dtos.ExamRules;
public class ExamRuleDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ExamCreationType ExamCreationType { get; set; }
    public string ProductName { get; set; }
    public Status Status { get; set; }
    public List<ExamRuleSubtopicDetailDto> ExamRuleSubtopics { get; set; }
}
