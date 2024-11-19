namespace BAExamApp.DataAccess.EFCore.Repositories;
public class TestExamRepository : EFBaseRepository<TestExam>, ITestExamRepository
{
    public TestExamRepository(BAExamAppDbContext context) : base(context) { }
}