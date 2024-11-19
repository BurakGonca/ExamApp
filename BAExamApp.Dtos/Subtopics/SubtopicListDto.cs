using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.Subtopics;
public class SubtopicListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SubjectName { get; set; }
    public Status Status { get; set; }
}
