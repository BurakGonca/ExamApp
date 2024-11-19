namespace BAExamApp.Dtos.TrainerProducts;
public class ProductAddTrainerDto
{
    public Guid ProductId { get; set; }
    public List<Guid> SelectedTrainersIds { get; set; }

}
