using BAExamApp.Core.Enums;
using BAExamApp.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.EFCore.Repositories;


public class StudentClassroomRepository : EFBaseRepository<StudentClassroom>, IStudentClassroomRepository
{
    public StudentClassroomRepository(BAExamAppDbContext context) : base(context)
    {
    }

    public async Task<List<StudentClassroom>> GetActiveStudentsByClassroomIdAsync(Guid classroomId)
    {
        return await _table.Where(sc => sc.ClassroomId == classroomId && sc.Status != Status.Deleted).ToListAsync(); //Sınıftaki aktif öğrencileri getirir.
    }
    public int CountActiveStudentClassrooms()
    {
        return _table.Count(sc => sc.Status != Status.Deleted);
    }
    public async Task UpdateRangeAsync(IEnumerable<StudentClassroom> studentClassrooms)
    {
        if (studentClassrooms == null || !studentClassrooms.Any())
            return;

        foreach (var studentClassroom in studentClassrooms)
        {
            _table.Entry(studentClassroom).State = EntityState.Modified;
        }
        await SaveChangesAsync();
    }
}
