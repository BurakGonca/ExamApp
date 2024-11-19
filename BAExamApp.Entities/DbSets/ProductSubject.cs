namespace BAExamApp.Entities.DbSets;

public class ProductSubject : BaseEntity
{
    //Navigation Prop.
    public Guid ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public Guid SubjectId { get; set; }
    public virtual Subject? Subject { get; set; }
}