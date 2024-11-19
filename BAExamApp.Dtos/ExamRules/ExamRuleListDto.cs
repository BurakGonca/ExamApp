using BAExamApp.Core.Enums;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.ExamRules;

public class ExamRuleListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ExamCreationType ExamCreationType { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedByPerson { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
}