namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IBranchRepository : IAsyncRepository, IAsyncQueryableRepository<Branch>, IAsyncFindableRepository<Branch>, IAsyncInsertableRepository<Branch>, IAsyncUpdateableRepository<Branch>, IAsyncDeleteableRepository<Branch>
{
}
