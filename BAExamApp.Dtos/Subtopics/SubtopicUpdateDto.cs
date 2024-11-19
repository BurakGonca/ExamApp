namespace BAExamApp.Dtos.Subtopics;
public class SubtopicUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Guid SubjectId { get; set; }

}
