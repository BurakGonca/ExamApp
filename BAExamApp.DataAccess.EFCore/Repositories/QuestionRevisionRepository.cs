using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class QuestionRevisionRepository : EFBaseRepository<QuestionRevision>, IQuestionRevisionRepository
{
    public QuestionRevisionRepository(BAExamAppDbContext context) : base(context)
    {
    }

    public async Task<QuestionRevision> GetActive()
    {
        return await _table.FirstOrDefaultAsync(x => x.Status == Core.Enums.Status.Added);
    }
}
