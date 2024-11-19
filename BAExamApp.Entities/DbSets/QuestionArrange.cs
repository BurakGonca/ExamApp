namespace BAExamApp.Entities.DbSets;

public class QuestionArrange : AuditableEntity
{
    public string Comment { get; set; }

    //Navigation Prop.
    public Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }
    public Guid ArrangerAdminId { get; set; }
    public virtual Admin? ArrangerAdmin { get; set; }
}
