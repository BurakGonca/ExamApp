using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.StudentClassrooms;

namespace BAExamApp.Dtos.Classrooms;

public class ClassroomDetailsDto
{
    public ClassroomDetailsDto()
    {
        StudentClassroomList = new List<StudentClassroomListForClassroomDetailsDto>();
        ExamClassroomList = new List<ExamClassroomForClassroomDetailsDto>();
        TrainerNames = new List<string>();
        ProductNames = new List<string>();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public string GroupTypeName { get; set; }
    public string BranchName { get; set; }
    public List<StudentClassroomListForClassroomDetailsDto> StudentClassroomList { get; set; }
    public List<ExamClassroomForClassroomDetailsDto> ExamClassroomList { get; set; }
    public List<string> TrainerNames { get; set; }
    public List<string> ProductNames { get; set; }
}