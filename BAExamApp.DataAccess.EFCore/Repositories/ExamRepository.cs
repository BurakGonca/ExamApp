using BAExamApp.Core.Enums;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class ExamRepository : EFBaseRepository<Exam>, IExamRepository
{
    public ExamRepository(BAExamAppDbContext context) : base(context)
    {
    }

    public int Count()
    {
        return _table.Count(e => e.Status != Status.Deleted && e.IsTest == false && e.IsCanceled == false);
    }
}