using BAExamApp.Core.Utilities.Results;
using System.Linq.Expressions;

namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IClassroomRepository : IAsyncRepository, IAsyncQueryableRepository<Classroom>, IAsyncFindableRepository<Classroom>, IAsyncUpdateableRepository<Classroom>, IAsyncInsertableRepository<Classroom>, IAsyncDeleteableRepository<Classroom>
{
    /// <summary>
    /// classrum'id ye göre bulur, include ile ilişkili entityleri getirir, ilişki yoksa deleted ilişki varsa pasif olarak sınıfın statusunu günceller
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResult> DeleteWithRelationship(Guid id);
    public Task<bool> HasRelate(Guid id);
    Task<IEnumerable<Classroom>> GetAllActiveClassrooms(Expression<Func<Classroom, bool>> expression, bool tracking = true);
}
