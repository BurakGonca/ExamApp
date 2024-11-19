namespace BAExamApp.Dtos.StudentClassrooms;
public class StudentAddToClassroomDto
{
    public Guid ClassroomId { get; set; }
    public List<Guid> SelectedStudentIds { get; set; }
}
