using BAExamApp.Entities.ApiEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories
{
    public interface IApiUserRepository : IAsyncRepository, IAsyncInsertableRepository<ApiUser>, IAsyncQueryableRepository<ApiUser>, IAsyncFindableRepository<ApiUser>, IAsyncDeleteableRepository<ApiUser>, IAsyncUpdateableRepository<ApiUser>, IAsyncTransactionRepository
    {
        Task<ApiUser?> GetByIdentityIdAsync(string identityId);
    }
}