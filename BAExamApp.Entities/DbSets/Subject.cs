namespace BAExamApp.Entities.DbSets;

public class Subject : AuditableEntity
{
    public Subject()
    {
        Subtopics = new HashSet<Subtopic>();
        ProductSubjects = new HashSet<ProductSubject>();
        Questions = new HashSet<Question>();
    }
    public string Name { get; set; } = null!;

    //Navigation Prop.

    public virtual ICollection<Question> Questions { get; set; }
    public virtual ICollection<Subtopic> Subtopics { get; set; }
    public virtual ICollection<ProductSubject> ProductSubjects { get; set; }
}
