using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Products;
using BAExamApp.Entities.DbSets;

namespace BAExamApp.Dtos.Subjects;
public class SubjectDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProductListDto> Products { get; set; }
    public virtual List<Subtopic>? Subtopics { get; set; } = new List<Subtopic>();
    public virtual List<ProductSubject> ProductSubjects { get; set; } = new List<ProductSubject>();
    public Status Status { get; set; }
}
