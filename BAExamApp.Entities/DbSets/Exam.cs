namespace BAExamApp.Entities.DbSets;

public class Exam : AuditableEntity
{
    public Exam()
    {
        StudentExams = new HashSet<StudentExam>();
        ExamEvaluators = new HashSet<ExamEvaluator>();
        ExamClassrooms = new HashSet<ExamClassrooms>();
    }

    public string Name { get; set; } = null!;
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public string? Description { get; set; }
    public bool? IsStarted { get; set; } = false;
    public ExamType? ExamType { get; set; }
    public ExamCreationType ExamCreationType { get; set; }
    public decimal MaxScore { get; set; } = 100;
    public decimal BonusScore { get; set; } = 0;
    public string? TrainerExplanation { get; set; }
    public bool? IsTest { get; set; } 
    public bool? IsCanceled { get; set; }

    //Navigation Prop.
    public Guid ExamRuleId { get; set; }
    public virtual ExamRule? ExamRule { get; set; }

    public virtual ICollection<StudentExam> StudentExams { get; set; }
    public virtual ICollection<ExamEvaluator> ExamEvaluators { get; set; }
    public virtual ICollection<ExamClassrooms> ExamClassrooms { get; set; }
}