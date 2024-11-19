using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.TrainerClassrooms;

namespace BAExamApp.Dtos.Classrooms;
public class ClassroomDetailsForAdminDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public Guid GroupTypeId { get; set; }
    public string GroupTypeName { get; set; }
    public Guid BranchId { get; set; }
   
    public string BranchName { get; set; }

    public List<Guid> ProductIds { get; set; }
    public List<string> ProductNames { get; set; }
    public List<StudentClassroomListForClassroomDetailsForAdminDto> StudentClassrooms { get; set; }
    public List<TrainerClassroomListForClassroomDetailsDto> TrainerClassrooms { get; set; }
}
