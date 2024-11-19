namespace BAExamApp.Entities.DbSets;

public class QuestionFeedback : AuditableEntity
{
    public string? Comment { get; set; }
    public bool? LikeStatus { get; set; }

    //Navigation Prop.
    public Guid QuestionId { get; set; }
    public virtual  Question? Question { get; set; }
}
