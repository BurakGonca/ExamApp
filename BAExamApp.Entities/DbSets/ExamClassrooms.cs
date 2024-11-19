namespace BAExamApp.Entities.DbSets;

public class ExamClassrooms : AuditableEntity
{
    public Guid ExamId { get; set; }
    public virtual Exam? Exam { get; set; }

    public Guid ClassroomId { get; set; }
    public virtual Classroom? Classroom { get; set; }
}
