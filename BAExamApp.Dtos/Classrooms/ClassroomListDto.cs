using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.Classrooms;

public class ClassroomListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public int StudentCount { get; set; }
    public string BranchName { get; set; }
    public bool IsActive { get; set; }
    public Status Status { get; set; }
    public int ClassroomExamCount { get; set; }
    public int ClassroomAppointedExamCount { get; set; }
}