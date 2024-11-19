using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.Exams;

public class ExamListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string> ClassroomNames { get; set; }
    public string ExamRuleName { get; set; }
    public bool? IsStarted { get; set; }
    public bool? IsTest { get; set; }


    public bool? IsCanceled { get; set; }
    public List<string> StudentName { get; set; }
    public List<decimal?> StudentExamScore { get; set; }
    public List<(string?, decimal?, string?)> tooltipStudentList { get; set; }
    public virtual ICollection<StudentExam> StudentExams { get; set; }
    public ExamType? ExamType { get; set; }
    public Guid ClassroomId { get; set; }
    public Guid RuleId { get; set; }
    public decimal? ClassGradeAverage { get; set; }
    public int? StudentCount { get; set; }
    public int? StudentExamScoreCount { get; set; }
    public decimal? StudentMaxScore { get; set; }
    public decimal? StudentMinScore { get; set; }
}