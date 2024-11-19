namespace BAExamApp.DataAccess.EFCore.Repositories;
public class ClassroomProductRepository : EFBaseRepository<ClassroomProduct>, IClassroomProductRepository
{
    public ClassroomProductRepository(BAExamAppDbContext context) : base(context)
    {

    }
}
