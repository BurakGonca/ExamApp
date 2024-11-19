using BAExamApp.Dtos.ClassroomProducts;
using Org.BouncyCastle.Crypto;

namespace BAExamApp.Business.Services;

public class ClassroomProductService : IClassroomProductService
{
    private readonly IClassroomProductRepository _classroomProductRepository;
    private readonly IMapper _mapper;

    public ClassroomProductService(IClassroomProductRepository classroomProductRepository, IMapper mapper)
    {
        _classroomProductRepository = classroomProductRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ClassroomProductListDto>>> GetAllByClassroomListAsync(List<Guid> ids)
    {
        var classroomProductList = new List<ClassroomProductListDto>();

        foreach (var id in ids)
        {
            var classroomProducts = await _classroomProductRepository.GetAllAsync(x => x.ClassroomId == id);
            classroomProductList.AddRange(_mapper.Map<List<ClassroomProductListDto>>(classroomProducts));
        }

        return new SuccessDataResult<List<ClassroomProductListDto>>(classroomProductList, Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<Guid>>> GetProductIdListByClassroomIdAsync(Guid id)
    {
        var classroomProducts = await _classroomProductRepository.GetAllAsync(x => x.ClassroomId == id);

        return new SuccessDataResult<List<Guid>>(classroomProducts.Select(x=>x.ProductId).ToList(), Messages.ListedSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var classroomProduct = await _classroomProductRepository.GetByIdAsync(id);

        if (classroomProduct is null)
        {
            return new ErrorDataResult<ClassroomProductDto>(Messages.ClassroomProductNotFound);
        }

        await _classroomProductRepository.DeleteAsync(classroomProduct);
        await _classroomProductRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IResult> DeleteRangeAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            var classroomProduct = await _classroomProductRepository.GetByIdAsync(id);

            if (classroomProduct is null)
            {
                return new ErrorDataResult<ClassroomProductDto>(Messages.ClassroomProductNotFound);
            }

            await _classroomProductRepository.DeleteAsync(classroomProduct);
        }

        await _classroomProductRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }
}
