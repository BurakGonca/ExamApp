namespace BAExamApp.Entities.DbSets;

public class StudentAnswer : BaseEntity
{
    public bool IsSelected { get; set; }

    //Navigation Prop.
    public Guid QuestionAnswerId { get; set; }
    public virtual QuestionAnswer? QuestionAnswer { get; set; }
    public Guid StudentQuestionId { get; set; }
    public virtual StudentQuestion? StudentQuestion { get; set; }

}
