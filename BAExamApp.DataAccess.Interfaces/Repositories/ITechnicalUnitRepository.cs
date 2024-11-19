namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface ITechnicalUnitRepository : IAsyncRepository, IAsyncQueryableRepository<TechnicalUnit>, IAsyncFindableRepository<TechnicalUnit>, IAsyncInsertableRepository<TechnicalUnit>, IAsyncUpdateableRepository<TechnicalUnit>, IAsyncDeleteableRepository<TechnicalUnit>
{
}
