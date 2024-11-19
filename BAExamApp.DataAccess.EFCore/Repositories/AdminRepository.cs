using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class AdminRepository : EFBaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(BAExamAppDbContext context) : base(context) { }

    public Task<Admin?> GetByIdentityIdAsync(string identityId)
    {
        return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }
}