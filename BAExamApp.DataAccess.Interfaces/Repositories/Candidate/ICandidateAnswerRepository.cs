using System.Linq.Expressions;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateAnswerRepository : IAsyncRepository, IAsyncInsertableRepository<CandidateAnswer>, IAsyncQueryableRepository<CandidateAnswer>, IAsyncDeleteableRepository<CandidateAnswer>, IAsyncFindableRepository<CandidateAnswer>, IAsyncUpdateableRepository<CandidateAnswer>
{
    Task<IEnumerable<CandidateAnswer>> GetAllWithIncludeAsync(Expression<Func<CandidateAnswer, bool>> expression, Expression<Func<CandidateAnswer, object>> include, bool tracking = true);
}
