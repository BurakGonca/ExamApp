using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.StudentClassrooms;
public class StudentClassroomListForClassroomDetailsForAdminDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int StudentExamCount { get; set; }
    public int StudentAppointedExamCount { get; set; }
    public decimal? StudentExamAvg { get; set; }


}
