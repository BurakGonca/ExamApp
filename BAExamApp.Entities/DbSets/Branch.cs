namespace BAExamApp.Entities.DbSets;

public class Branch : AuditableEntity
{
    public Branch()
    {
        Classrooms = new HashSet<Classroom>();
    }

    public string Name { get; set; } = null!;

    //Navigation Prop.
    public virtual ICollection<Classroom> Classrooms { get; set; }
}