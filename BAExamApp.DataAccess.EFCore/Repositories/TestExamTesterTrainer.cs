namespace BAExamApp.DataAccess.EFCore.Repositories;
internal class TestExamTesterTrainer : EFBaseRepository<TestExamTester>, ITestExamTesterRepository
{
    public TestExamTesterTrainer(BAExamAppDbContext context) : base(context) { }
}