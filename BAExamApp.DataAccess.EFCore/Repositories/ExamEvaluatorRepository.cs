namespace BAExamApp.DataAccess.EFCore.Repositories;

public class ExamEvaluatorRepository : EFBaseRepository<ExamEvaluator>, IExamEvaluatorRepository
{
    public ExamEvaluatorRepository(BAExamAppDbContext context) : base(context) { }
}
