using BAExamApp.Dtos.Exams;

namespace BAExamApp.Dtos.TrainerClassrooms;
public class TrainerClassroomExamListDto
{
    public string Name { get; set; }
    public List<ExamListDto> Exams { get; set; }
}
