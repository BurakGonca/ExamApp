
using Microsoft.EntityFrameworkCore;
using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class StudentExamRepository : EFBaseRepository<StudentExam>, IStudentExamRepository
{
    private readonly BAExamAppDbContext _context;

    public StudentExamRepository(BAExamAppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<StudentExam>> GetStudentExamsByClassroomIdAsync(Guid classroomId)
    {
        var result = await _context.Students_Classrooms
                                   .Include(x => x.Student)
                                   .ThenInclude(y => y.StudentExams)
                                   .Where(x => x.ClassroomId == classroomId)
                                   .SelectMany(x => x.Student.StudentExams)
                                   .ToListAsync();
        return result;
    }
    public async Task<List<StudentExam>> GetStudentExams(Guid id)
    {
        var ogrenciSinavlar = await _context.Students_Exams
                    .Include(x => x.Exam)
                    .Where(x => x.StudentId == id)
                    .ToListAsync();
        return ogrenciSinavlar;
    }
    public async Task<List<StudentExam>> GetStudentExams(string email)
    {
        var studentExams = await _context.Students_Exams
                    .Include(x => x.Exam)
                    .Where(x => x.Student.Email == email)
                    .ToListAsync();
        return studentExams;

    }

    public async Task<List<StudentExam>> GetStudentExamsByTrainerIdAsync(Guid trainerId)
    {
        var trainerClassrooms = await _context.Trainers_Classrooms
                                      .Where(tc => tc.TrainerId == trainerId)
                                      .Include(tc => tc.Classroom)
                                      .ToListAsync();

        var classrooms = trainerClassrooms.Select(tc => tc.Classroom).ToList();

        var examClassrooms = classrooms.SelectMany(c => _context.ExamClassrooms
                                                        .Include(ec => ec.Exam)
                                                        .Where(ec => ec.ClassroomId == c.Id))
                                       .ToList();

        var studentExams = examClassrooms.SelectMany(ec => _context.Students_Exams
                                                 .Include(se => se.Student)
                                                 .Include(se => se.Exam)
                                                 .Where(se => se.ExamId == ec.ExamId))
                       .ToList();

        return studentExams;

    }


    /// <summary>
    /// StudentExam tablosundaki studentId ile Student tablosundaki id'yi karşılaştırır. 
    /// </summary>
    /// <param name="studentExamId"></param>
    /// <returns>Eşit olduğu durumda Student tablosundaki Emaili döndürür.</returns>
    public async Task<string> GetStudentEmailByStudentExamAsync(Guid studentExamId)
    {
        var studentEmail = await (from se in _context.Students_Exams
                                  join s in _context.Students
                                  on se.StudentId equals s.Id
                                  where se.Id == studentExamId
                                  select s.Email).FirstOrDefaultAsync();

        return studentEmail;
    }



    public async Task<List<StudentExam>> GetUnsendedStudentExamsAsync()
    {
        //var exams = await _context.Students_Exams
        //.Where(se => !se.IsEmailSent && se.Status == Status.Active && se.IsFinished)
        //.ToListAsync();

        var exams = await _context.Students_Exams
    .FromSqlRaw("SELECT * FROM Students_Exams WHERE IsEmailSent = 0 AND Status = 3")
    .ToListAsync();

        return exams;
    }
}