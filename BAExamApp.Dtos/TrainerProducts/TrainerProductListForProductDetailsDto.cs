namespace BAExamApp.Dtos.TrainerProducts;
public class TrainerProductListForProductDetailsDto
{
    public Guid TrainerId { get; set; }
    public Guid ProductId { get; set; }
    public string? FullName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
