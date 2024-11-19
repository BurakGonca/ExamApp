using BAExamApp.Entities.ApiEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories
{
    public class ApiUserRepository : EFBaseRepository<ApiUser>, IApiUserRepository
    {
        public ApiUserRepository(BAExamAppDbContext context) : base(context)
        {

        }

        public Task<ApiUser?> GetByIdentityIdAsync(string identityId)
        {
            return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
        }
    }
}