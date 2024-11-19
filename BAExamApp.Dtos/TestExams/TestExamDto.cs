using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.TestExams;
public class TestExamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime TestExamDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan TestExamDuration { get; set; }
    public State State { get; set; }
    public string? Description { get; set; }
}
