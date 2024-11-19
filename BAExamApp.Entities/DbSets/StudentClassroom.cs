namespace BAExamApp.Entities.DbSets;

public class StudentClassroom : AuditableEntity
{
    //Navigation Prop.
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public Guid ClassroomId { get; set; }
    public virtual Classroom? Classroom { get; set; }
}
