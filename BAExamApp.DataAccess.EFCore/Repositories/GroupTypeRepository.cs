namespace BAExamApp.DataAccess.EFCore.Repositories;

public class GroupTypeRepository : EFBaseRepository<GroupType>, IGroupTypeRepository
{
    public GroupTypeRepository(BAExamAppDbContext context) : base(context) { }
}