using BAExamApp.Dtos.QuestionArranges;
using BAExamApp.Dtos.QuestionRevisions;

namespace BAExamApp.Business.Services;
public class QuestionArrangeService : IQuestionArrangeService
{
    private readonly IQuestionArrangeRepository _questionArrangeRepository;
    private readonly IMapper _mapper;

    public QuestionArrangeService(IQuestionArrangeRepository questionArrangeRepository, IMapper mapper)
    {
        _questionArrangeRepository = questionArrangeRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Onaylanan Sorular için yapılan düzenleme yorumlarının databaseye eklenmesini sağlar.
    /// </summary>
    /// <param name="questionArrangeCreate"></param>
    /// <returns></returns>
    public async Task<IDataResult<QuestionArrangeDto>> AddAsync(QuestionArrangeCreateDto questionArrangeCreate)
    {
        await _questionArrangeRepository.AddAsync(_mapper.Map<QuestionArrange>(questionArrangeCreate));
        await _questionArrangeRepository.SaveChangesAsync();
        var questionArrangedto = _mapper.Map<QuestionArrangeDto>(questionArrangeCreate);
        return new SuccessDataResult<QuestionArrangeDto>(questionArrangedto, Messages.AddSuccess);
    }

    /// <summary>
    /// Onaylanan sorunun id'sine göre o soruya yapılan düzenleme yorumlarını getirir
    /// </summary>
    /// <param name="id">Düzenleme yapılan sorunun id'si</param>
    /// <returns></returns>
    public async Task<List<QuestionArrangeListDto>> GetAllByQuestionIdAsync(Guid id)
    {
        return _mapper.Map<List<QuestionArrangeListDto>>(await _questionArrangeRepository.GetAllAsync(questionArrange => questionArrange.QuestionId == id));
    }
}
