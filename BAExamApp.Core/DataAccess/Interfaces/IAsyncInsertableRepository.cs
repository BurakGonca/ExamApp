namespace BAExamApp.Core.DataAccess.Interfaces;

public interface IAsyncInsertableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
}
