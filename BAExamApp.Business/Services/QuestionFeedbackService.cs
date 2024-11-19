using BAExamApp.Dtos.QuestionFeedbacks;
using BAExamApp.Entities.DbSets;

namespace BAExamApp.Business.Services;
public class QuestionFeedbackService : IQuestionFeedbackService
{
    private readonly IQuestionFeedbackRepository _questionFeedbackRepository;
    private IMapper _mapper;

    public QuestionFeedbackService(IQuestionFeedbackRepository questionFeedbackRepository, IMapper mapper)
    {
        _questionFeedbackRepository = questionFeedbackRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<QuestionFeedbackListDto>>> GetAllByQuestionIdAsync(Guid id)
    {
        var questionFeedbacks = await _questionFeedbackRepository.GetAllAsync(x => x.QuestionId == id);

        return new SuccessDataResult<List<QuestionFeedbackListDto>>(_mapper.Map<List<QuestionFeedbackListDto>>(questionFeedbacks), Messages.ListedSuccess);
    }

    public async Task<IDataResult<QuestionFeedbackDto>> AddAsync(QuestionFeedbackCreateDto questionFeedbackCreate)
    {
        var questionFeedback = _mapper.Map<QuestionFeedback>(questionFeedbackCreate);

        await _questionFeedbackRepository.AddAsync(questionFeedback);
        await _questionFeedbackRepository.SaveChangesAsync();

        return new SuccessDataResult<QuestionFeedbackDto>(_mapper.Map<QuestionFeedbackDto>(questionFeedback), Messages.AddSuccess);
    }
}
