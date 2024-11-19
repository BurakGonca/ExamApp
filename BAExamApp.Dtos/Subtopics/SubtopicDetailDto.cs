using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.Subtopics;
public class SubtopicDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool? IsActive { get; set; }
    public Guid SubjectId { get; set; }
    public Status Status { get; set; }
}
