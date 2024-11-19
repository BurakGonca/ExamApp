namespace BAExamApp.Entities.DbSets;

public class ExamEvaluator : BaseEntity
{
    //Navigation Prop.
    public Guid ExamId { get; set; }
    public virtual Exam? Exam { get; set; }
    public Guid TrainerId { get; set; }
    public virtual Trainer? Trainer { get; set; }
}
