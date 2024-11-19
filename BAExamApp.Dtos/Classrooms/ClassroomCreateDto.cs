using BAExamApp.Dtos.ClassroomProducts;

namespace BAExamApp.Dtos.Classrooms;

public class ClassroomCreateDto
{
    public string Name { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public Guid GroupTypeId { get; set; }
    public Guid BranchId { get; set; }
    public List<ClassroomProductCreateDto> ClassroomProducts { get; set; }
}