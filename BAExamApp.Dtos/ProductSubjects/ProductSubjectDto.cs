using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.ProductSubjects;

public class ProductSubjectDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid? SubjectId { get; set; }
}
