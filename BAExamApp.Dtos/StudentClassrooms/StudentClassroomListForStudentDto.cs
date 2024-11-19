using System.ComponentModel.DataAnnotations;

namespace BAExamApp.Dtos.StudentClassrooms;
public class StudentClassroomListForStudentDto
{
    public Guid ClassroomId { get; set; }
    public string ClassroomName { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public string BranchName { get; set; }
}
