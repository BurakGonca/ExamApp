using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.Exams;

public class ExamCreateDto
{
    public string Name { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public string? Description { get; set; }
    public ExamType ExamType { get; set; }
    public ExamCreationType ExamCreationType { get; set; }
    public decimal MaxScore { get; set; }
    public decimal BonusScore { get; set; }
    public bool? IsStarted { get; set; } = false;
    public bool? IsTest { get; set; }
    public bool? IsCanceled { get; set; }
    public Guid ExamRuleId { get; set; }
    public List<StudentExamCreateDto> StudentExams { get; set; }
    public List<ExamClassroomsCreateDto> ExamClassroomsIds { get; set; }
}