using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.Subjects;

public class SubjectListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
}