using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class QuestionArrangeRepository : EFBaseRepository<QuestionArrange>, IQuestionArrangeRepository
{
    public QuestionArrangeRepository(BAExamAppDbContext context) : base(context)
    {
    }
}
