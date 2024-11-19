using BAExamApp.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class StudentRepository : EFBaseRepository<Student>, IStudentRepository
{
    private readonly BAExamAppDbContext _context;

    public StudentRepository(BAExamAppDbContext context) : base(context)
    {
        _context = context;
    }

    public int Count()
    {
        return _table.Count(s => s.Status != Status.Deleted);
    }

    public Task<Student?> GetByIdentityIdAsync(string identityId)
    {
        return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }
    public async Task<Student> GetStudentByEmail(string email)
    {
        var student = await _context.Students.Where(x => x.Email == email).FirstOrDefaultAsync();
        return student;
    }
}