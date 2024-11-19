using BAExamApp.Core.Utilities.Results;
using BAExamApp.Dtos.ProductSubjects;

namespace BAExamApp.Business.Interfaces.Services;
public interface IProductSubjectService
{
    Task<IDataResult<ProductSubjectDto>> AddAsync(ProductSubjectCreateDto productSubjectCreateDto);
    Task<IDataResult<List<ProductSubjectDto>>> AddRangeAsync(List<ProductSubjectCreateDto> productSubjectsCreateDto);
    Task<IResult> DeleteAsync(Guid id);
    Task<IResult> DeleteRangeAsync(List<Guid> ids);
    Task<IDataResult<List<ProductSubjectDto>>> GetAll();
}
