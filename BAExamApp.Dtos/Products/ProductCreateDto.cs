using BAExamApp.Dtos.ProductSubjects;

namespace BAExamApp.Dtos.Products;

public class ProductCreateDto
{
    public string Name { get; set; }
    public bool? IsActive { get; set; }
    public Guid TechnicalUnitId { get; set; }
}