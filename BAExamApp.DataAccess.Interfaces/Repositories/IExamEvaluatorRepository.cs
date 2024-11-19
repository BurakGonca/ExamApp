namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IExamEvaluatorRepository : IRepository, IAsyncRepository, IAsyncQueryableRepository<ExamEvaluator>, IAsyncFindableRepository<ExamEvaluator>, IAsyncUpdateableRepository<ExamEvaluator>, IAsyncInsertableRepository<ExamEvaluator>, IAsyncDeleteableRepository<ExamEvaluator>
{
}
