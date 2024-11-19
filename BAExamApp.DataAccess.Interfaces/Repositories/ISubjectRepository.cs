namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface ISubjectRepository : IAsyncRepository, IAsyncQueryableRepository<Subject>, IAsyncFindableRepository<Subject>, IAsyncInsertableRepository<Subject>, IAsyncDeleteableRepository<Subject>,IAsyncUpdateableRepository<Subject>
{
}
