namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IQuestionAnswerRepository : IAsyncRepository, IAsyncInsertableRepository<QuestionAnswer>, IAsyncQueryableRepository<QuestionAnswer>, IAsyncDeleteableRepository<QuestionAnswer>, IAsyncFindableRepository<QuestionAnswer>, IRepository,IAsyncUpdateableRepository<QuestionAnswer>, IAsyncTransactionRepository
{
}
