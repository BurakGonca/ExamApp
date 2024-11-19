namespace BAExamApp.DataAccess.EFCore.Repositories;
public class TestExamQuestionRepository : EFBaseRepository<TestExamQuestion>, ITestExamQuestionRepository
{
    public TestExamQuestionRepository(BAExamAppDbContext context) : base(context) { }
}