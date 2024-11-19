using BAExamApp.Dtos.ProductSubjects;
using BAExamApp.Dtos.TrainerProducts;

namespace BAExamApp.Dtos.Products;

public class ProductDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool? IsActive { get; set; }
    public string TechnicalUnitName { get; set; }
    public int ClassroomCount { get; set; }
    public List<TrainerProductListForProductDetailsDto> TrainerProducts { get; set; }
    public List<ProductSubjectListDto> ProductSubjects { get; set; }
    public bool IsClassroomUsed { get; set; }
    public bool IsTrainerUsed { get; set; }
    public bool IsExamRuleUsed { get; set; }
}