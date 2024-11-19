namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface IEmailRepository : IRepository, IAsyncQueryableRepository<Email>, IAsyncFindableRepository<Email>, IAsyncUpdateableRepository<Email>, IAsyncInsertableRepository<Email>, IAsyncDeleteableRepository<Email>
{
}
