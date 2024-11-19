namespace BAExamApp.DataAccess.EFCore.Repositories;

public class TechnicalUnitRepository : EFBaseRepository<TechnicalUnit>, ITechnicalUnitRepository
{
    public TechnicalUnitRepository(BAExamAppDbContext context) : base(context) { }
}