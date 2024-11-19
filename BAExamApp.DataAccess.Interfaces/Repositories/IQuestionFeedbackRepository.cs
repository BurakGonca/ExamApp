namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IQuestionFeedbackRepository:  IAsyncRepository, IAsyncInsertableRepository<QuestionFeedback>, IAsyncQueryableRepository<QuestionFeedback>, IAsyncDeleteableRepository<QuestionFeedback>, IAsyncFindableRepository<QuestionFeedback>, IAsyncUpdateableRepository<QuestionFeedback>, IRepository
{
}
