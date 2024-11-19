namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IQuestionDifficultyRepository : IAsyncRepository, IAsyncInsertableRepository<QuestionDifficulty>,
  IAsyncQueryableRepository<QuestionDifficulty>, IAsyncFindableRepository<QuestionDifficulty>, IAsyncUpdateableRepository<QuestionDifficulty>, IAsyncDeleteableRepository<QuestionDifficulty>
{
}
