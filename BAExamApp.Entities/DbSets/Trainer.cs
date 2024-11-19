namespace BAExamApp.Entities.DbSets;

public class Trainer : BaseUser
{
    public Trainer()
    {
        StudentExams = new HashSet<StudentExam>();
        TrainerClassrooms = new HashSet<TrainerClassroom>();
        TrainerProducts = new HashSet<TrainerProduct>();
        ExamEvaluators = new HashSet<ExamEvaluator>();
        QuestionRevisions = new HashSet<QuestionRevision>();
        TestExamTesters = new HashSet<TestExamTester>();
    }
    public byte[]? NewImage { get; set; }
    //Navigation Prop.
    //public Guid? TechnicalUnitId { get; set; }
    public virtual TechnicalUnit? TechnicalUnit { get; set; }


    public virtual ICollection<StudentExam> StudentExams { get; set; }
    public virtual ICollection<TrainerClassroom> TrainerClassrooms { get; set; }
    public virtual ICollection<TrainerProduct> TrainerProducts { get; set; }
    public virtual ICollection<ExamEvaluator> ExamEvaluators { get; set; }
    public virtual ICollection<QuestionRevision> QuestionRevisions { get; set; }
    public virtual ICollection<TestExamTester> TestExamTesters { get; set; }
}
