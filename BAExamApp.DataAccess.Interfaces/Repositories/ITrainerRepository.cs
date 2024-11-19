namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface ITrainerRepository : IAsyncRepository, IAsyncInsertableRepository<Trainer>, IAsyncFindableRepository<Trainer>, IAsyncDeleteableRepository<Trainer>, IAsyncUpdateableRepository<Trainer>, IAsyncTransactionRepository
{
    Task<Trainer?> GetByIdentityIdAsync(string identityId);
    Task<List<Trainer>> GetAllTrainers();
    Task<List<Trainer>> GetAllTrainersNonDeleted();
    int Count();
}