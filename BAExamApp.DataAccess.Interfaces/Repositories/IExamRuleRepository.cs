using System.Linq.Expressions;

namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IExamRuleRepository : IAsyncRepository, IAsyncQueryableRepository<ExamRule>, IAsyncFindableRepository<ExamRule>, IAsyncInsertableRepository<ExamRule>, IAsyncDeleteableRepository<ExamRule>, IAsyncUpdateableRepository<ExamRule>
{
    Task<IEnumerable<ExamRule>> GetAllAsync(Expression<Func<ExamRule, object>> include, bool tracking = true);
}
