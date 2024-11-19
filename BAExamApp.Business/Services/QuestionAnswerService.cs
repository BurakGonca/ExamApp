using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Dtos.ClassroomProducts;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.QuestionAnswers;
using Castle.Components.DictionaryAdapter.Xml;

namespace BAExamApp.Business.Services;
public class QuestionAnswerService : IQuestionAnswerService
{
    private readonly IQuestionAnswerRepository _questionAnswerRepository;
    private readonly IMapper _mapper;
    private readonly IStudentAnswerRepository _studentAnswerRepository;

    public QuestionAnswerService(IQuestionAnswerRepository questionAnswerRepository, IMapper mapper)
    {
        _questionAnswerRepository = questionAnswerRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<QuestionAnswerDto>> GetById(Guid id,bool traking=true)
    {
        var questionAnswer = await _questionAnswerRepository.GetByIdAsync(id,traking);
        if (questionAnswer == null)
        {
            return new ErrorDataResult<QuestionAnswerDto>(Messages.QuestionAnswerNotFound);
        }

        return new SuccessDataResult<QuestionAnswerDto>(_mapper.Map<QuestionAnswerDto>(questionAnswer), Messages.FoundSuccess);
    }

    public async Task<IDataResult<QuestionAnswerDto>> AddAsync(QuestionAnswerCreateDto questionAnswerCreateDto)
    {
        var questionAnswer = _mapper.Map<QuestionAnswer>(questionAnswerCreateDto);

        await _questionAnswerRepository.AddAsync(questionAnswer);
        await _questionAnswerRepository.SaveChangesAsync();

        return new SuccessDataResult<QuestionAnswerDto>(_mapper.Map<QuestionAnswerDto>(questionAnswer), Messages.AddSuccess);
    }

    public async Task<IDataResult<List<QuestionAnswerDto>>> AddRangeAsync(List<QuestionAnswerCreateDto> questionAnswersCreateDto)
    {
        var questionAnswers = new List<QuestionAnswer>();

        foreach (var questionAnswerCreateDto in questionAnswersCreateDto)
        {
            var questionAnswer = _mapper.Map<QuestionAnswer>(questionAnswerCreateDto);

            await _questionAnswerRepository.AddAsync(questionAnswer);

            questionAnswers.Add(questionAnswer);
        }
        await _questionAnswerRepository.SaveChangesAsync();

        return new SuccessDataResult<List<QuestionAnswerDto>>(_mapper.Map<List<QuestionAnswerDto>>(questionAnswers), Messages.AddSuccess);
    }

    public async Task<IDataResult<List<QuestionAnswerDto>>> UpdateRangeAsync(List<QuestionAnswerCreateDto> questionAnswersUpdateDto)
    {
        if (questionAnswersUpdateDto.Count > 0)
        {
            var CurrentQuestionAnswers = await _questionAnswerRepository.GetAllAsync(x => x.QuestionId == questionAnswersUpdateDto[0].QuestionId);
            await DeleteRangeAsync(CurrentQuestionAnswers.Select(x => x.Id).ToList());
        }

        return await AddRangeAsync(questionAnswersUpdateDto);
    }

    public async Task<IDataResult<List<QuestionAnswerDto>>> Update(List<QuestionAnswerDto> questionAnswersUpdateDto)
    {
        if (questionAnswersUpdateDto.Count > 0)
        {
            foreach (var updatedDto in questionAnswersUpdateDto)
            {
                var existingEntity = await _questionAnswerRepository.GetAsync(x => x.Id == updatedDto.Id);

                if (existingEntity != null)
                {
                    // Güncelleme işlemi
                    var updatedAnswer=_mapper.Map(updatedDto, existingEntity);

                    await _questionAnswerRepository.UpdateAsync(updatedAnswer);
                    
                }
                else
                {
                    return new ErrorDataResult<List<QuestionAnswerDto>>();
                }
            }
            await _questionAnswerRepository.SaveChangesAsync();
        }

        return new SuccessDataResult<List<QuestionAnswerDto>>();
    }


    public async Task<IResult> DeleteAsync(Guid id)
    {
        var questionAnswer = await _questionAnswerRepository.GetByIdAsync(id);

        if (questionAnswer is null)
        {
            return new ErrorDataResult<ClassroomProductDto>(Messages.ClassroomProductNotFound);
        }

        await _questionAnswerRepository.DeleteAsync(questionAnswer);
        await _questionAnswerRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IResult> DeleteRangeAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            var questionAnswer = await _questionAnswerRepository.GetByIdAsync(id);

            if (questionAnswer is null)
            {
                return new ErrorDataResult<ClassroomProductDto>(Messages.ClassroomProductNotFound);
            }

            await _questionAnswerRepository.DeleteAsync(questionAnswer);
        }

        await _questionAnswerRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<List<QuestionAnswerDto>>> GetByQuestionId(Guid Id)
    {
        var answers = await _questionAnswerRepository.GetAllAsync(x => x.QuestionId == Id);

        return new SuccessDataResult<List<QuestionAnswerDto>>(_mapper.Map<List<QuestionAnswerDto>>(answers), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<QuestionAnswerListDto>>> GetAllAsync()
    {
        var answers = await _questionAnswerRepository.GetAllAsync();

        return new SuccessDataResult<List<QuestionAnswerListDto>>(_mapper.Map<List<QuestionAnswerListDto>>(answers), Messages.ListedSuccess);
    }
}
