namespace BAExamApp.Core.DataAccess.Interfaces;

public interface IAsyncUpdateableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<TEntity> UpdateAsync(TEntity entity);
}
