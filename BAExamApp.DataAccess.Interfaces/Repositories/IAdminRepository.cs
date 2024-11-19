namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IAdminRepository : IAsyncRepository, IAsyncFindableRepository<Admin>, IAsyncInsertableRepository<Admin>, IAsyncDeleteableRepository<Admin>, IAsyncUpdateableRepository<Admin>, IAsyncTransactionRepository
{
    Task<Admin?> GetByIdentityIdAsync(string identityId);
}
