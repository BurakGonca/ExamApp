using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class ExamRuleRepository : EFBaseRepository<ExamRule>, IExamRuleRepository
{
    public ExamRuleRepository(BAExamAppDbContext context) : base(context) { }

    public async Task<IEnumerable<ExamRule>> GetAllAsync(Expression<Func<ExamRule, object>> include, bool tracking = true)
    {
        return await GetAllActives(tracking).Include(include).ToListAsync();
    }
}