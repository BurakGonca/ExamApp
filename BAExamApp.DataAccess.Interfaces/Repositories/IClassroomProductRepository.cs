namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface IClassroomProductRepository : IAsyncRepository, IAsyncInsertableRepository<ClassroomProduct>, IAsyncFindableRepository<ClassroomProduct>, IAsyncDeleteableRepository<ClassroomProduct>, IAsyncUpdateableRepository<ClassroomProduct>, IAsyncTransactionRepository
{
}
