using BAExamApp.Business.Interfaces.Services;
using BAExamApp.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class TrainerRepository : EFBaseRepository<Trainer>, ITrainerRepository
{
    public TrainerRepository(BAExamAppDbContext context) : base(context) { }

    public Task<Trainer?> GetByIdentityIdAsync(string identityId)
    {
        return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }

    public Task<List<Trainer>> GetAllTrainers()
    {
        return _table.ToListAsync();
    }
    public Task<List<Trainer>> GetAllTrainersNonDeleted()
    {
        return _table.Where(x => x.Status != Status.Deleted).ToListAsync();
    }
    public int Count()
    {
        return _table.Count(t=> t.Status != Status.Deleted);
    }
}