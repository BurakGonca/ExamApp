namespace BAExamApp.Entities.DbSets.Candidates;

public class CandidatesGroups : AuditableEntity
{
    // Candidate ve candidategroup tablolarındaki many to many bağlantısını kurmak için gereken tablo 
    // entity frameworkun oluşturması yerine elle oluşturulma sebebi daha iyi müdahale edilip yönetilebilmesi


    // NAV PROP
    public Guid CandidateId { get; set; }
    public Guid CandidateGroupId { get; set; }

    public virtual CandidateCandidate Candidate { get; set; }
    public virtual CandidateCandidateGroup CandidateGroup { get; set; }
}
