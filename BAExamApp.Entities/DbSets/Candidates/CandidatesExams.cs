namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidatesExams : AuditableEntity
{
    // Candidate ve CandidateCandidateExam tablolarındaki many to many bağlantısını kurmak için gereken tablo 
    // entity frameworkun oluşturması yerine elle oluşturulma sebebi daha iyi müdahale edilip yönetilebilmesi


    // NAV PROP
    public Guid CandidateId { get; set; }
    public Guid CandidateExamId { get; set; }
    //public Guid CandidateQuestionId { get; set; }

    public virtual CandidateCandidate Candidate { get; set; }
    public virtual CandidateExam CandidateExam { get; set; }
    //public virtual CandidateCandidateQuestion CandidateQuestion { get; set; } 

    public DateTime? StartDate { get; set; }
    public bool  IsExamStarted { get; set; }
    public bool  IsExamFinished { get; set; }
}
