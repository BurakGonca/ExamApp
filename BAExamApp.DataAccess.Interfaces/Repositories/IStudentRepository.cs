namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IStudentRepository : IAsyncRepository, IAsyncInsertableRepository<Student>, IAsyncQueryableRepository<Student>, IAsyncFindableRepository<Student>, IAsyncDeleteableRepository<Student>, IAsyncUpdateableRepository<Student>, IAsyncTransactionRepository
{
    Task<Student?> GetByIdentityIdAsync(string identityId);
    int Count();
    Task<Student> GetStudentByEmail(string email);

}
