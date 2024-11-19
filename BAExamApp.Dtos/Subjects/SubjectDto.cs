using BAExamApp.Entities.DbSets;

namespace BAExamApp.Dtos.Subjects;

public class SubjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual List<Subtopic>? Subtopics { get; set; } = new List<Subtopic>();

    public virtual List<ProductSubject> ProductSubjects { get; set; } = new List<ProductSubject>();
}