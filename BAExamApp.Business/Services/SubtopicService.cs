using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Subtopics;

namespace BAExamApp.Business.Services;
public class SubtopicService : ISubtopicService
{
    private readonly ISubtopicRepository _subtopicRepository;
    private readonly IQuestionSubtopicsRepository _questionSubtopicsRepository;
    private readonly IExamRuleSubtopicRepository _examRuleSubtopicRepository;
    private readonly IMapper _mapper;

    public SubtopicService(ISubtopicRepository subtopicRepository, IMapper mapper, IQuestionSubtopicsRepository questionSubtopicsRepository, IExamRuleSubtopicRepository examRuleSubtopicRepository)
    {
        _subtopicRepository = subtopicRepository;
        _questionSubtopicsRepository = questionSubtopicsRepository;
        _examRuleSubtopicRepository = examRuleSubtopicRepository;   
        _mapper = mapper;
    }
    
   
    public async Task<IDataResult<SubtopicDto>> AddAsync(SubtopicCreateDto subtopicCreateDto)
    {
        if (await _subtopicRepository.AnyAsync(x => x.Name.ToLower().Equals(subtopicCreateDto.Name.Trim().ToLower()) && x.SubjectId == subtopicCreateDto.SubjectId))
        {
            return new ErrorDataResult<SubtopicDto>(Messages.SubtopicAlreadyExist);
        }
        var subtopic = _mapper.Map<Subtopic>(subtopicCreateDto);
        await _subtopicRepository.AddAsync(subtopic);
        await _subtopicRepository.SaveChangesAsync();
        return new SuccessDataResult<SubtopicDto>(_mapper.Map<SubtopicDto>(subtopic), Messages.AddSuccess);
    }

   
    public async Task<IResult> DeleteAsync(Guid subtopicId)
    {
        var subtopic = await _subtopicRepository.GetByIdAsync(subtopicId);
        if (subtopic is null)
        {
            return new ErrorDataResult<SubtopicDto>(Messages.ProductNotFound);
        }

        var examRuleUsingSuptopic = await IsRuleUsedSubtopicAsync(subtopic.Id);

        var questionUsingSuptopic = await IsQuestionUsedSubtopicAsync(subtopic.Id);

        if ( examRuleUsingSuptopic || questionUsingSuptopic)
        {
            subtopic.Status = Core.Enums.Status.Passive;
            await _subtopicRepository.SaveChangesAsync();
            return new SuccessResult(Messages.ChangeStatusSuccess); 
        }

         

        await _subtopicRepository.DeleteAsync(subtopic);
        await _subtopicRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }
   
    public async Task<IDataResult<List<SubtopicListDto>>> GetAllAsync()
    {
        var subtopics = await _subtopicRepository.GetAllAsync();
        return new SuccessDataResult<List<SubtopicListDto>>(_mapper.Map<List<SubtopicListDto>>(subtopics), Messages.ListedSuccess);

    }
    
    public async Task<IDataResult<List<SubtopicListDto>>> GetSubtopicBySubjectId(Guid subjectId)
    {
        List<SubtopicListDto> dtoslist = new List<SubtopicListDto>();
        var SubtopicList = await _subtopicRepository.GetAllAsync(x => x.SubjectId == subjectId);
        foreach (var subtopic in SubtopicList)
        {
            SubtopicListDto subtopicListDtosub = new SubtopicListDto()
            {
                Id = subtopic.Id,
                Name = subtopic.Name,
            };
            dtoslist.Add(subtopicListDtosub);
        }
        return new SuccessDataResult<List<SubtopicListDto>>(_mapper.Map<List<SubtopicListDto>>(dtoslist), Messages.FoundSuccess);
    }


    public async Task<IDataResult<SubtopicDetailDto>> GetDetailsByIdAsync(Guid id)
    {
        var subtopic = await _subtopicRepository.GetAsync(x=>x.Id== id);
        var subtopicDetailDto = _mapper.Map<SubtopicDetailDto>(subtopic);
        
        return new SuccessDataResult<SubtopicDetailDto>(subtopicDetailDto, Messages.FoundSuccess);
    }

    
    public async Task<IDataResult<SubtopicDto>> GetSubtopicById(Guid id)
    {
        var subtopic = await _subtopicRepository.GetByIdAsync(id);
        if (subtopic is null)
        {
            return new ErrorDataResult<SubtopicDto>(Messages.SubtopicNotFound);
        }
        var subtopicDto = _mapper.Map<SubtopicDto>(subtopic);
        
        return new SuccessDataResult<SubtopicDto>(subtopicDto,Messages.FoundSuccess);

    }

   
    public async Task<IDataResult<List<SubtopicListDto>>> GetAllBySubtopicsAsync(Guid productId)
    {
        List<SubtopicListDto> allSubtopicsListDtoList = new List<SubtopicListDto>();
        var allSubtopics = await _subtopicRepository.GetAllAsync();
        foreach (var subtopic in allSubtopics)
        {
            SubtopicListDto subtopicListDto = _mapper.Map<SubtopicListDto>(subtopic);           
            allSubtopicsListDtoList.Add(subtopicListDto);
        }
        return new SuccessDataResult<List<SubtopicListDto>>(_mapper.Map<List<SubtopicListDto>>(allSubtopicsListDtoList), Messages.FoundSuccess);
    }
    
    public async Task<IDataResult<SubtopicDto>> UpdateAsync(SubtopicUpdateDto entity)
    {
       var subtopic = await _subtopicRepository.GetByIdAsync(entity.Id);
        if (subtopic is null)
        {
            return new ErrorDataResult<SubtopicDto>(Messages.SubtopicNotFound);
        }
        var updatedSubtopic = _mapper.Map(entity, subtopic);
        
        await _subtopicRepository.UpdateAsync(updatedSubtopic);
        await _subtopicRepository.SaveChangesAsync();

        return new SuccessDataResult<SubtopicDto>(_mapper.Map<SubtopicDto>(updatedSubtopic),Messages.UpdateSuccess);
        
    }

    /// <summary>
    /// Sınav kuralının alt kurala bağlı olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="subtopicId"></param>
    /// <returns></returns>

    public async Task<bool> IsRuleUsedSubtopicAsync(Guid subtopicId)
    {
        var ruleUsingSubtopic = await _examRuleSubtopicRepository.AnyAsync(e => e.SubtopicId == subtopicId);
        return ruleUsingSubtopic;
    }

    /// <summary>
    /// Alt konunun soruya bağlı olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="subtopicId"></param>
    /// <returns></returns>

    public async Task<bool> IsQuestionUsedSubtopicAsync(Guid subtopicId)
    {
        var questionUsingSubtopic = await _questionSubtopicsRepository.AnyAsync(e => e.SubtopicId == subtopicId);
        return questionUsingSubtopic;
    }

    /// <summary>
    /// Alt konunun statüsünü değiştirir.
    /// </summary>
    /// <param name="subtopicId"></param>
    /// <returns></returns>
    public async Task<IResult> ChangeRuleStatusAsync(Guid subtopicId)
    {
        var subtopic = await _subtopicRepository.GetByIdAsync(subtopicId);
        if(subtopic == null)
        {
            return new ErrorResult(Messages.SubtopicNotFound);
        }

        subtopic.Status = subtopic.Status == Status.Active ? Status.Passive : Status.Active;
        try
        {
            await _subtopicRepository.SaveChangesAsync();
        }
        catch (Exception)
        {

            return new ErrorResult(Messages.ChangeStatusFail); 
        }
        return new SuccessResult(Messages.ChangeStatusSuccess); 
    }


}
