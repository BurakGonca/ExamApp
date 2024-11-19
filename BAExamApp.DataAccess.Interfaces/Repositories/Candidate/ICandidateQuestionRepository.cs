using System.Linq.Expressions;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateQuestionRepository : IAsyncRepository, IAsyncInsertableRepository<CandidateQuestion>, IAsyncQueryableRepository<CandidateQuestion>, IAsyncFindableRepository<CandidateQuestion>, IAsyncUpdateableRepository<CandidateQuestion>, IAsyncDeleteableRepository<CandidateQuestion>
{
    Task<IEnumerable<CandidateQuestion>> GetAllWithIncludeAsync(Expression<Func<CandidateQuestion, bool>> expression, Expression<Func<CandidateQuestion, object>> include, bool tracking = true);

    Task<CandidateQuestion> GetByIdWithIncludeAsync(Expression<Func<CandidateQuestion, bool>> expression, Expression<Func<CandidateQuestion, object>> include, bool tracking = true);

    Task<IEnumerable<CandidateQuestion>> GetBySubjectIdAsync(Guid subjectId);
}
