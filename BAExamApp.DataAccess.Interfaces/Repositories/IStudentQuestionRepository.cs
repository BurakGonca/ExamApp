namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IStudentQuestionRepository : IRepository, IAsyncRepository, IAsyncQueryableRepository<StudentQuestion>, IAsyncFindableRepository<StudentQuestion>, IAsyncUpdateableRepository<StudentQuestion>, IAsyncInsertableRepository<StudentQuestion>
{
}
