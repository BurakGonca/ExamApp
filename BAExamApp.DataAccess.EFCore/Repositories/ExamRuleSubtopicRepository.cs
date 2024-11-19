namespace BAExamApp.DataAccess.EFCore.Repositories;
public class ExamRuleSubtopicRepository : EFBaseRepository<ExamRuleSubtopic>, IExamRuleSubtopicRepository
{
    public ExamRuleSubtopicRepository(BAExamAppDbContext context) : base(context) { }
}