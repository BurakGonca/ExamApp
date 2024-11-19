namespace BAExamApp.DataAccess.EFCore.Repositories;

public class ProductSubjectRepository : EFBaseRepository<ProductSubject>, IProductSubjectRepository
{
    public ProductSubjectRepository(BAExamAppDbContext context) : base(context) { }
}