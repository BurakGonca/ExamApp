using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.Products;

public class ProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool? IsActive { get; set; }
    public Status Status { get; set; }
}