namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface ISubtopicRepository : IAsyncRepository, IAsyncInsertableRepository<Subtopic>, IAsyncQueryableRepository<Subtopic>, IAsyncDeleteableRepository<Subtopic>, IAsyncFindableRepository<Subtopic>, IAsyncUpdateableRepository<Subtopic>
{
}
