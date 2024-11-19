using BAExamApp.Core.Enums;
using BAExamApp.Dtos.ProductSubjects;

namespace BAExamApp.Dtos.Products;

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool? IsActive { get; set; }
    public Guid TechnicalUnitId { get; set; }
    public List<ProductSubjectDto>? ProductSubjects { get; set; }
    public Status Status { get; set; }
}
