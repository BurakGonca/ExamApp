namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IExamClassroomsRepository : IRepository, IAsyncRepository, IAsyncQueryableRepository<ExamClassrooms>, IAsyncFindableRepository<ExamClassrooms>, IAsyncUpdateableRepository<ExamClassrooms>, IAsyncInsertableRepository<ExamClassrooms>, IAsyncDeleteableRepository<ExamClassrooms>
{
    Task<List<ExamClassrooms>> GetActiveStudensInClassroomAsync (Guid classroomId);
}
