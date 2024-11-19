namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface ITrainerProductRepository : IAsyncRepository, IAsyncInsertableRepository<TrainerProduct>, IAsyncQueryableRepository<TrainerProduct>, IAsyncDeleteableRepository<TrainerProduct>, IAsyncFindableRepository<TrainerProduct>
{
}
