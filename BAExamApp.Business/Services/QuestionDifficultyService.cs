using BAExamApp.Dtos.QuestionDifficulty;

namespace BAExamApp.Business.Services;

public class QuestionDifficultyService : IQuestionDifficultyService
{
    private readonly IQuestionDifficultyRepository _questionDiffucultyRepository;
    private readonly IMapper _mapper;

    public QuestionDifficultyService(IQuestionDifficultyRepository questionDiffucultyRepository, IMapper mapper)
    {
        _questionDiffucultyRepository = questionDiffucultyRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<QuestionDifficultyListDto>>> GetAllAsync()
    {
        var questionDifficulties = await _questionDiffucultyRepository.GetAllAsync();

        return new SuccessDataResult<List<QuestionDifficultyListDto>>(_mapper.Map<List<QuestionDifficultyListDto>>(questionDifficulties), Messages.ListedSuccess);
    }

    public async Task<IDataResult<QuestionDifficultyDto>> GetDetailsByIdAsync(Guid id)
    {
        var questionDifficulty = await _questionDiffucultyRepository.GetByIdAsync(id);

        if (questionDifficulty is null)
        {
            return new ErrorDataResult<QuestionDifficultyDto>(Messages.QuestionNotFound);
        }

        return new SuccessDataResult<QuestionDifficultyDto>(_mapper.Map<QuestionDifficultyDto>(questionDifficulty), Messages.FoundSuccess);
    }

    public async Task<IDataResult<QuestionDifficultyDto>> AddAsync(QuestionDifficultyCreateDto questionDifficultyCreateDto)
    {
        var hasQuestionDifficulty = await _questionDiffucultyRepository.AnyAsync(x => x.Name.ToLower().Equals(questionDifficultyCreateDto.Name.ToLower()));

        if (hasQuestionDifficulty)
        {
            return new ErrorDataResult<QuestionDifficultyDto>(Messages.AddFailAlreadyExists);
        }

        var questionDifficulty = _mapper.Map<QuestionDifficulty>(questionDifficultyCreateDto);

        await _questionDiffucultyRepository.AddAsync(questionDifficulty);
        await _questionDiffucultyRepository.SaveChangesAsync();

        return new SuccessDataResult<QuestionDifficultyDto>(_mapper.Map<QuestionDifficultyDto>(questionDifficulty), Messages.AddSuccess);
    }

    public async Task<IDataResult<QuestionDifficultyDto>> UpdateAsync(QuestionDifficultyUpdateDto questionDifficultyUpdateDto)
    {
        var questionDifficulty = await _questionDiffucultyRepository.GetByIdAsync(questionDifficultyUpdateDto.Id);

        if (questionDifficulty is null)
        {
            return new ErrorDataResult<QuestionDifficultyDto>(Messages.QuestionNotFound);
        }

        var updatequestionDifficulty = _mapper.Map(questionDifficultyUpdateDto, questionDifficulty);

        await _questionDiffucultyRepository.UpdateAsync(updatequestionDifficulty);
        await _questionDiffucultyRepository.SaveChangesAsync();

        return new SuccessDataResult<QuestionDifficultyDto>(_mapper.Map<QuestionDifficultyDto>(updatequestionDifficulty), Messages.UpdateSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var hasQuestionDifficulty = await _questionDiffucultyRepository.GetByIdAsync(id);

        if (hasQuestionDifficulty is null)
        {
            return new ErrorDataResult<QuestionDifficultyDto>(Messages.QuestionNotFound);
        }

        await _questionDiffucultyRepository.DeleteAsync(hasQuestionDifficulty);
        await _questionDiffucultyRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }
}
