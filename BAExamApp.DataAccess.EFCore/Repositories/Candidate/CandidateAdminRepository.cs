using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;
namespace BAExamApp.DataAccess.EFCore.Repositories.Candidate;
public class CandidateAdminRepository : EFBaseRepository<CandidateCandidateAdmin>, ICandidateAdminRepository
{
    public CandidateAdminRepository(BAExamAppDbContext context) : base(context) { }
    public Task<CandidateCandidateAdmin?> GetByIdentityIdAsync(string identityId)
    {
        return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }
}
