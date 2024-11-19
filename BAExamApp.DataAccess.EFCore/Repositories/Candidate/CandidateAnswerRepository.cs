using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BAExamApp.DataAccess.EFCore.Repositories.Candidate;
public class CandidateAnswerRepository : EFBaseRepository<CandidateAnswer>, ICandidateAnswerRepository
{
    public CandidateAnswerRepository(BAExamAppDbContext context) : base(context) 
    { 
    }

    public async Task<IEnumerable<CandidateAnswer>> GetAllWithIncludeAsync(Expression<Func<CandidateAnswer, bool>> expression, Expression<Func<CandidateAnswer, object>> include, bool tracking = true)
    {
        return await GetAllActives(tracking).Include(include).Where(expression).ToListAsync();
    }
}
