using BAExamApp.Dtos.GroupTypes;

namespace BAExamApp.Business.Interfaces.Services;

public interface IGroupTypeService
{
    Task<IDataResult<GroupTypeDto>> GetByIdAsync(Guid id);
    Task<IDataResult<List<GroupTypeListDto>>> GetAllAsync();
    Task<IDataResult<GroupTypeDto>> AddAsync(GroupTypeCreateDto groupTypeCreateDto);
    Task<IDataResult<GroupTypeDto>> UpdateAsync(GroupTypeUpdateDto groupTypeUpdateDto);
    Task<IResult> DeleteAsync(Guid id);
}
