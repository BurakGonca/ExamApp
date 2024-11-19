namespace BAExamApp.Dtos.Classrooms;

public class ClassroomUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public Guid GroupTypeId { get; set; }
    public Guid BranchId { get; set; }
    public List<Guid> ProductIds { get; set; }
    //public List<ClassroomProductUpdateDto> ClassroomProducts { get; set; }
}