using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BAExamApp.DataAccess.EFCore.Repositories.Candidate;
public class CandidateQuestionRepository : EFBaseRepository<CandidateQuestion>, ICandidateQuestionRepository
{
    public CandidateQuestionRepository(BAExamAppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CandidateQuestion>> GetAllWithIncludeAsync(Expression<Func<CandidateQuestion, bool>> expression, Expression<Func<CandidateQuestion, object>> include, bool tracking = true)
    {
        return await GetAllActives(tracking).Include(include).Where(expression).ToListAsync();
    }

    public async Task<CandidateQuestion> GetByIdWithIncludeAsync(Expression<Func<CandidateQuestion, bool>> expression, Expression<Func<CandidateQuestion, object>> include, bool tracking = true)
    {
        return await GetAllActives(tracking).Include(include).Where(expression).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CandidateQuestion>> GetBySubjectIdAsync(Guid subjectId)
    {
        return await GetAllWithIncludeAsync(
            cq => cq.CandidateQuestionSubjectId == subjectId,
            cq => cq.QuestionAnswers,
            true);
    }
}
