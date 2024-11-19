using BAExamApp.Dtos.ProductSubjects;

namespace BAExamApp.Dtos.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool? IsActive { get; set; }
    public Guid TechnicalUnitId { get; set; }
    public List<ProductSubjectDto>? ProductSubjects { get; set; }
}