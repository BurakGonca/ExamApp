namespace BAExamApp.Business.Services;

public class ExamRuleSubtopicService : IExamRuleSubtopicService
{
    private readonly IExamRuleSubtopicRepository _examRuleSubtopicRepository;
    private readonly IMapper _mapper;
    public ExamRuleSubtopicService(IExamRuleSubtopicRepository examRuleSubtopicRepository, IMapper mapper)
    {
        _examRuleSubtopicRepository = examRuleSubtopicRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ExamRuleSubtopic>>> GetAllAsync(Guid id)
    {
        var examRuleSubtopics = await _examRuleSubtopicRepository.GetAllAsync(x=>x.ExamRuleId==id);

        if (examRuleSubtopics is null)
        {
            return new ErrorDataResult<List<ExamRuleSubtopic>>(Messages.ExamRuleSubtopicNotFound);
        }

        return new SuccessDataResult<List<ExamRuleSubtopic>>(_mapper.Map<List<ExamRuleSubtopic>>(examRuleSubtopics), Messages.FoundSuccess);
    }
}
