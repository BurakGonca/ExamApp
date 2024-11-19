using BAExamApp.Dtos.ClassroomProducts;

namespace BAExamApp.Business.Interfaces.Services;

public interface IClassroomProductService
{
    Task<IDataResult<List<Guid>>> GetProductIdListByClassroomIdAsync(Guid id);
    Task<IDataResult<List<ClassroomProductListDto>>> GetAllByClassroomListAsync(List<Guid> ids);
    Task<IResult> DeleteAsync(Guid id);
    Task<IResult> DeleteRangeAsync(List<Guid> ids);
}
