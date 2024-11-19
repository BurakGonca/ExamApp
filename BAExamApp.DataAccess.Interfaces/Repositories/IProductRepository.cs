namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IProductRepository : IAsyncRepository, IAsyncInsertableRepository<Product>, IAsyncQueryableRepository<Product>, IAsyncFindableRepository<Product>,
  IAsyncUpdateableRepository<Product>, IAsyncDeleteableRepository<Product>
{
}
