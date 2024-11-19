using BAExamApp.Entities.DbSets.Candidates;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateAdminRepository : IAsyncRepository, IAsyncFindableRepository<CandidateCandidateAdmin>, IAsyncInsertableRepository<CandidateCandidateAdmin>, IAsyncDeleteableRepository<CandidateCandidateAdmin>, IAsyncUpdateableRepository<CandidateCandidateAdmin>, IAsyncTransactionRepository
{
    Task<CandidateCandidateAdmin?> GetByIdentityIdAsync(string identityId);
}
