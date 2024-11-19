using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.ProductSubjects;
using BAExamApp.Dtos.Subjects;

namespace BAExamApp.Business.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IProductSubjectRepository _productsSubjectsRepository;
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;
    private readonly ISubtopicRepository _subtopicRepository;
    private readonly IProductSubjectRepository _productSubjectRepository;
    public SubjectService(ISubjectRepository subjectRepository, IProductSubjectRepository productsSubjectsRepository, IMapper mapper, IQuestionRepository questionRepository, ISubtopicRepository subtopicRepository, IProductSubjectRepository productSubjectRepository)
    {
        _subjectRepository = subjectRepository;
        _productsSubjectsRepository = productsSubjectsRepository;
        _mapper = mapper;
        _questionRepository = questionRepository;
        _subtopicRepository = subtopicRepository;
        _productSubjectRepository = productSubjectRepository;
    }

    public async Task<IDataResult<List<SubjectListDto>>> GetAllAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync(false);
        return new SuccessDataResult<List<SubjectListDto>>(_mapper.Map<List<SubjectListDto>>(subjects), Messages.ListedSuccess);
    }

    public async Task<IDataResult<SubjectDto>> AddAsync(SubjectCreateDto subjectCreateDto)
    {
        var productSubjects = subjectCreateDto.ProductSubjects;
        var subjectExists =await _subjectRepository.AnyAsync(x => x.Name.ToLower().Equals(subjectCreateDto.Name.Trim().ToLower()));

        if (subjectExists)
        {
            var existingSubject= await _subjectRepository.GetAsync(x => x.Name.ToLower().Equals(subjectCreateDto.Name.Trim().ToLower()));
            var subjectId = existingSubject.Id;

            var newProductSubjects = new List<ProductSubjectDto>();

            foreach (var productSubject in productSubjects)
            {
                var exists = await _productsSubjectsRepository.AnyAsync(x => x.ProductId.Equals(productSubject.ProductId) && x.SubjectId.Equals(subjectId));

                if (!exists)
                {
                    productSubject.SubjectId = subjectId;
                    newProductSubjects.Add(productSubject);
                }
            }

            if (newProductSubjects.Any())
            {
                var newProductSubjectEntities = _mapper.Map<IEnumerable<ProductSubject>>(newProductSubjects);
                await _productsSubjectsRepository.AddRangeAsync(newProductSubjectEntities);
                await _productsSubjectsRepository.SaveChangesAsync();
            }
            else
            {
                return new ErrorDataResult<SubjectDto>(Messages.SubjectAlreadyExist);
            }

            return new SuccessDataResult<SubjectDto>(_mapper.Map<SubjectDto>(existingSubject), Messages.AddSuccess);
        }

        var subject = _mapper.Map<Subject>(subjectCreateDto);
        await _subjectRepository.AddAsync(subject);
        await _subjectRepository.SaveChangesAsync();
        return new SuccessDataResult<SubjectDto>(_mapper.Map<SubjectDto>(subject), Messages.AddSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid subjectId)
    {
        var subject = await _subjectRepository.GetByIdAsync(subjectId);

        if (subject is null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }

        // Bağlantılı varlıkların status değerlerini güncelle
        subject.Questions.ToList().ForEach(q => q.Status = Core.Enums.Status.Deleted);
        subject.Subtopics.ToList().ForEach(st => st.Status = Core.Enums.Status.Deleted);
        subject.ProductSubjects.ToList().ForEach(ps => ps.Status = Core.Enums.Status.Deleted);

        // Subject'in status değerini güncelle
        subject.Status = Core.Enums.Status.Deleted;

        await _subjectRepository.DeleteAsync(subject);
        await _subjectRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<SubjectDto>> GetByIdAsync(Guid id)
    {
        var subject = await _subjectRepository.GetByIdAsync(id);
        if (subject is null)
        {
            return new ErrorDataResult<SubjectDto>(Messages.SubjectNotFound);
        }

        return new SuccessDataResult<SubjectDto>(_mapper.Map<SubjectDto>(subject), Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<SubjectListDto>>> GetAllByProductIdAsync(Guid productId)
    {
        var productSubjects = await _productsSubjectsRepository.GetAllAsync(x => x.ProductId == productId);
        var subjects = productSubjects.Select(x => x.Subject).ToList();

        var subjectListDto = _mapper.Map<List<SubjectListDto>>(subjects);
        return new SuccessDataResult<List<SubjectListDto>>(subjectListDto, Messages.ListReceived);
    }

    public async Task<IDataResult<List<SubjectListDto>>> GetAllByListProductIdsAsync(List<Guid> productIds)
    {
        List<ProductSubject> productSubjectList = new List<ProductSubject>();
        foreach (var productId in productIds)
        {
            var productSubjects = await _productsSubjectsRepository.GetAllAsync(x => x.ProductId == productId);

            foreach (var productSubject in productSubjects)
            {
                productSubjectList.Add(productSubject);
            }

        }
        var subjects = productSubjectList.Select(x => x.Subject).ToList();

        var subjectListDto = _mapper.Map<List<SubjectListDto>>(subjects);
        return new SuccessDataResult<List<SubjectListDto>>(subjectListDto, Messages.ListReceived);
    }


    public async Task<IDataResult<SubjectDto>> UpdateAsync(SubjectUpdateDto entity)
    {
        if (await _subjectRepository.AnyAsync(x => x.Name.ToLower().Equals(entity.Name.Trim().ToLower()) && x.Id != entity.Id))
        {
            return new ErrorDataResult<SubjectDto>(Messages.SubjectAlreadyExist);
        }

        var subject = await _subjectRepository.GetByIdAsync(entity.Id);
        var updatedSubject = _mapper.Map(entity, subject);
        await _subjectRepository.UpdateAsync(updatedSubject);
        await _subjectRepository.SaveChangesAsync();
        return new SuccessDataResult<SubjectDto>(_mapper.Map<SubjectDto>(updatedSubject), Messages.UpdateSuccess);
    }

    public async Task<IDataResult<SubjectDetailDto>> GetDetailsByIdAsync(Guid id)
    {
        var subject = await _subjectRepository.GetByIdAsync(id);
        if (subject is null)
            return new ErrorDataResult<SubjectDetailDto>(Messages.SubjectNotFound);

        var subjectDetailDto = _mapper.Map<SubjectDetailDto>(subject);
        subjectDetailDto.Products = new();
        foreach (var subjectProduct in subject.ProductSubjects)
        {
            subjectDetailDto.Products.Add(_mapper.Map<ProductListDto>(subjectProduct.Product));
        }
        return new SuccessDataResult<SubjectDetailDto>(subjectDetailDto, Messages.FoundSuccess);
    }

    public async Task<bool> IsQuestionUsedInSubjectAsync(Guid subjectId)
    {
        var questionUsingSubject = await _questionRepository.AnyAsync(e => e.SubjectId == subjectId);
        return questionUsingSubject;
    }

    public async Task<bool> IsSubtopicUsedInSubjectAsync(Guid subjectId)
    {
        var subtopicUsingSubject = await _subtopicRepository.AnyAsync(e => e.SubjectId == subjectId);
        return subtopicUsingSubject;
    }

    public async Task<bool> IsProductSubjectUsedInSubjectAsync(Guid subjectId)
    {
        var productSubjectUsingSubject = await _productSubjectRepository.AnyAsync(e => e.SubjectId == subjectId);
        return productSubjectUsingSubject;
    }

    public async Task<IResult> ChangeSubjectStatusAsync(Guid subjectId)
    {
        var subject = await _subjectRepository.GetByIdAsync(subjectId);

        if (subject is null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }

        subject.Status = subject.Status == Core.Enums.Status.Active ? Core.Enums.Status.Passive : Core.Enums.Status.Active;

        try
        {
            await _subjectRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.ChangeStatusFail);
        }

        return new SuccessResult(Messages.ChangeStatusSuccess);
    }


}