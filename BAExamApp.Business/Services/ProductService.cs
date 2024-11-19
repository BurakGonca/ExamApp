using BAExamApp.Dtos.Products;

namespace BAExamApp.Business.Services;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IClassroomProductRepository _classroomProductRepository;
    private readonly IProductSubjectRepository _productSubjectRepository;
    private readonly IExamRuleRepository _examRuleRepository;
    private readonly ITrainerProductRepository _trainerProductRepository;

    public ProductService(IProductRepository productRepository, IMapper mapper, IClassroomProductRepository classroomProductRepository, IProductSubjectRepository productSubjectRepository, IExamRuleRepository examRuleRepository, ITrainerProductRepository trainerProductRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _classroomProductRepository = classroomProductRepository;
        _productSubjectRepository = productSubjectRepository;
        _examRuleRepository = examRuleRepository;
        _trainerProductRepository = trainerProductRepository;
    }
    public async Task<IDataResult<ProductDto>> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return new ErrorDataResult<ProductDto>(Messages.ProductNotFound);
        }

        return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(product), Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<ProductListDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync(false);

        return new SuccessDataResult<List<ProductListDto>>(_mapper.Map<List<ProductListDto>>(products), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<ProductListDto>>> GetAllBySubjectIdAsync(Guid id)
    {
        var products = await _productRepository.GetAllAsync(x => x.ProductSubjects.Any(s => s.SubjectId == id));

        return new SuccessDataResult<List<ProductListDto>>(_mapper.Map<List<ProductListDto>>(products), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<ProductListDto>>> GetAllByTechnicalUnitIdAsync(Guid id)
    {
        var products = await _productRepository.GetAllAsync(x => x.TechnicalUnitId == id);

        return new SuccessDataResult<List<ProductListDto>>(_mapper.Map<List<ProductListDto>>(products), Messages.ListedSuccess);
    }

    public async Task<IDataResult<ProductDetailDto>> GetDetailsByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return new ErrorDataResult<ProductDetailDto>(Messages.ProductNotFound);
        }

        return new SuccessDataResult<ProductDetailDto>(_mapper.Map<ProductDetailDto>(product), Messages.FoundSuccess);
    }

    public async Task<IDataResult<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
    {
        var hasProduct = await _productRepository.AnyAsync(product => product.Name.ToLower() == productCreateDto.Name.Trim().ToLower());

        if (hasProduct)
        {
            return new ErrorDataResult<ProductDto>(Messages.AddFailAlreadyExists);
        }

        var product = _mapper.Map<Product>(productCreateDto);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(product), Messages.AddSuccess);
    }
    
    public async Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
    {
        var product = await _productRepository.GetByIdAsync(productUpdateDto.Id);

        if (product is null)
        {
            return new ErrorDataResult<ProductDto>(Messages.ProductNotFound);
        }

        var updatedProduct = _mapper.Map(productUpdateDto, product);

        await _productRepository.UpdateAsync(updatedProduct);
        await _productRepository.SaveChangesAsync();

        return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(updatedProduct), Messages.UpdateSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return new ErrorDataResult<ProductDto>(Messages.ProductNotFound);
        }

        var classroomUsingProduct = await IsClassroomUsedProductAsync(product.Id);
        var trainerUsingProduct = await IsTrainerUsedProductAsync(product.Id);
        var examRuleUsingProduct = await IsExamRuleUsedProductAsync(product.Id);

        if (classroomUsingProduct || trainerUsingProduct || examRuleUsingProduct)
        {
            product.Status = Core.Enums.Status.Passive;
            await _productRepository.SaveChangesAsync();
            return new SuccessResult(Messages.SetIsActiveFalse);
        }

        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<bool> IsClassroomUsedProductAsync(Guid productId)
    {
        var classroomUsingProduct = await _classroomProductRepository.AnyAsync(e => e.ProductId == productId);
        return classroomUsingProduct;
    }
    public async Task<bool> IsTrainerUsedProductAsync(Guid productId)
    {
        var trainerUsingProduct = await _trainerProductRepository.AnyAsync(e => e.ProductId == productId);
        return trainerUsingProduct;
    }
    public async Task<bool> IsExamRuleUsedProductAsync(Guid productId)
    {
        var examRuleUsingProduct = await _examRuleRepository.AnyAsync(e => e.ProductId == productId);
        return examRuleUsingProduct;
    }
}
