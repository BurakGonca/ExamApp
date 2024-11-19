using BAExamApp.Dtos.StudentExams;

namespace BAExamApp.Dtos.Students;
public class StudentDetailsForTrainerDto
{
    public Guid Id { get; set; }
    public string StudentName { get; set; }
    public List<StudentExamListForTrainerDto> StudentExams { get; set; }
   
}
