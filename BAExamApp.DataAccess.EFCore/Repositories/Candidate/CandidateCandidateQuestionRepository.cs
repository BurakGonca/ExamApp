using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Entities.DbSets.Candidates;

namespace BAExamApp.DataAccess.EFCore.Repositories.Candidate;
public class CandidateCandidateQuestionRepository : EFBaseRepository<CandidateCandidateQuestion>, ICandidateCandidateQuestionRepository
{
    public CandidateCandidateQuestionRepository(BAExamAppDbContext context) : base(context) { }
}
