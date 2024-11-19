using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Entities.DbSets.Candidates;

namespace BAExamApp.DataAccess.EFCore.Repositories.Candidate;
public class CandidateCandidateAnswerRepository : EFBaseRepository<CandidateCandidateAnswer>, ICandidateCandidateAnswerRepository
{
    public CandidateCandidateAnswerRepository(BAExamAppDbContext context) : base(context)
    {
    }
}
