namespace BAExamApp.DataAccess.EFCore.Repositories;

public class ProductRepository : EFBaseRepository<Product>, IProductRepository
{
    public ProductRepository(BAExamAppDbContext context) : base(context) { }
}