namespace BAExamApp.Entities.DbSets;

public class ClassroomProduct : AuditableEntity
{
    //Navigation Prop.
    public Guid ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public Guid ClassroomId { get; set; }
    public virtual Classroom? Classroom { get; set; }
}
