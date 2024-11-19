namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface ITestExamRepository : IAsyncRepository, IAsyncInsertableRepository<TestExam>, IAsyncQueryableRepository<TestExam>, IAsyncDeleteableRepository<TestExam>, IAsyncFindableRepository<TestExam>, IAsyncUpdateableRepository<TestExam>
{
}
