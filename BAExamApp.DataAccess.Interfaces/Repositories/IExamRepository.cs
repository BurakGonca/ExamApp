namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IExamRepository : IAsyncRepository, IAsyncInsertableRepository<Exam>, IAsyncQueryableRepository<Exam>, IAsyncDeleteableRepository<Exam>,IAsyncFindableRepository<Exam>,IAsyncUpdateableRepository<Exam>
{
    int Count();
}
