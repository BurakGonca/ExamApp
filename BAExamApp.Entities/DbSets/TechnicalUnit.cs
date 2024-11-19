namespace BAExamApp.Entities.DbSets;

public class TechnicalUnit : AuditableEntity
{
    public TechnicalUnit()
    {
        Trainers = new HashSet<Trainer>();
        Products = new HashSet<Product>();
    }

    public string Name { get; set; } = null!;

    //Navigation Prop.
    public virtual ICollection<Trainer> Trainers { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}