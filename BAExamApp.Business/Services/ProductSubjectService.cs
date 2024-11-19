using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.ProductSubjects;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Business.Services;
public class ProductSubjectService : IProductSubjectService
{
    private readonly IProductSubjectRepository _productSubjectRepository;
    private readonly IMapper _mapper;

    public ProductSubjectService(IProductSubjectRepository productSubjectRepository, IMapper mapper)
    {
        _productSubjectRepository = productSubjectRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<ProductSubjectDto>> AddAsync(ProductSubjectCreateDto productSubjectCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task<IDataResult<List<ProductSubjectDto>>> AddRangeAsync(List<ProductSubjectCreateDto> productSubjectsCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> DeleteRangeAsync(List<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public async Task<IDataResult<List<ProductSubjectDto>>> GetAll()
    {
        var productSubjects = await _productSubjectRepository.GetAllAsync();
        return new SuccessDataResult<List<ProductSubjectDto>>(_mapper.Map<List<ProductSubjectDto>>(productSubjects), Messages.ListedSuccess);
    }
}
