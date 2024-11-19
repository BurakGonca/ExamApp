namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidateExam : AuditableEntity
{
    // Area name + Entity name = Candidate + CandidateExam
    // Adaylara yapılan sınavları tutmak için

    public CandidateExam()
    {
        CandidatesExams = new HashSet<CandidatesExams>();
    }

    public string Name { get; set; } = null!;
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public string? Description { get; set; }
    public bool IsStarted { get; set; } = false;
    public int MaxScore { get; set; } = 100;
    public string? TrainerExplanation { get; set; }

    public int TestQuestionCount { get; set; }
    public int ClassicQuestionCount { get; set; }
    public int AlgorithmQuestionCount { get; set; }

    public DateTime? ExamLinkEndDate { get; set; }


    // NAV PROP
    public virtual ICollection<CandidatesExams> CandidatesExams { get; set; }
}
