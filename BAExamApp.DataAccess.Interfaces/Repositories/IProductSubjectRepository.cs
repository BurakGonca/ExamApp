using BAExamApp.Core.Entities.Interfaces;

namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IProductSubjectRepository : IAsyncRepository, IAsyncInsertableRepository<ProductSubject>, IAsyncQueryableRepository<ProductSubject>, IAsyncFindableRepository<ProductSubject>
{
}
