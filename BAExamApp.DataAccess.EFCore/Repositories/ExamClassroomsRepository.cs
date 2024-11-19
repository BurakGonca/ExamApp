
using BAExamApp.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class ExamClassroomsRepository : EFBaseRepository<ExamClassrooms>, 
IExamClassroomsRepository
{
    public ExamClassroomsRepository(BAExamAppDbContext context) : base(context) { }

    public async Task<List<ExamClassrooms>> GetActiveStudensInClassroomAsync(Guid classroomId)
    {
        return await _table.Where(sc => sc.ClassroomId == classroomId && sc.Status != Status.Deleted).ToListAsync(); //Sınıftaki aktif öğrencileri getirir.
    }
    public int CountActiveStudentClassrooms()
    {
        return _table.Count(sc => sc.Status != Status.Deleted);
    }
}

