namespace BAExamApp.Dtos.Classrooms;

public class ClassroomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public bool IsActive { get; set; }
    public Guid GroupTypeId { get; set; }
    public Guid BranchId { get; set; }
}