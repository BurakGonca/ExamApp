namespace BAExamApp.DataAccess.EFCore.Repositories;

public class BranchRepository : EFBaseRepository<Branch>, IBranchRepository
{
    public BranchRepository(BAExamAppDbContext context) : base(context)
    {
    }
}
