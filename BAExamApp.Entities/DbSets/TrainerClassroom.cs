namespace BAExamApp.Entities.DbSets;

public class TrainerClassroom : AuditableEntity
{
    public DateTime AssignedDate { get; set; }
    public DateTime? ResignedDate { get; set; }

    //Navigation Prop.
    public Guid TrainerId { get; set; }
    public virtual Trainer? Trainer { get; set; }
    public Guid ClassroomId { get; set; }
    public virtual Classroom? Classroom { get; set; }
}