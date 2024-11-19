namespace BAExamApp.Entities.DbSets;

public class GroupType : AuditableEntity
{
    public GroupType()
    {
        Classrooms = new HashSet<Classroom>();
    }

    public string Name { get; set; } = null!;
    public string Information { get; set; } = null!;

    //Navigation Prop.
    public virtual ICollection<Classroom> Classrooms { get; set; }
}
