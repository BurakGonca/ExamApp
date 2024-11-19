using Microsoft.EntityFrameworkCore;

namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IStudentExamRepository : IRepository, IAsyncRepository, IAsyncQueryableRepository<StudentExam>, IAsyncFindableRepository<StudentExam>, IAsyncUpdateableRepository<StudentExam>, IAsyncInsertableRepository<StudentExam>, IAsyncDeleteableRepository<StudentExam>, IAsyncTransactionRepository

{
    Task<List<StudentExam>> GetStudentExams(string email);
    Task<List<StudentExam>> GetStudentExamsByClassroomIdAsync(Guid classroomId);
    Task<string> GetStudentEmailByStudentExamAsync(Guid studentExamId);
    Task<List<StudentExam>> GetStudentExamsByTrainerIdAsync(Guid trainerId);

    /// <summary>
    /// StudentExam tablosundan mail gönreilmemiş, sınavı tamamlamış ve statüsü aktif olan sınavları getirir.
    /// </summary>
    /// <returns></returns>
    Task<List<StudentExam>> GetUnsendedStudentExamsAsync();
}