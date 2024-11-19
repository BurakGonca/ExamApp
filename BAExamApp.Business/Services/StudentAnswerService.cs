using BAExamApp.Dtos.StudentAnswers;

namespace BAExamApp.Business.Services;
public class StudentAnswerService : IStudentAnswerService
{ 
    private readonly IStudentAnswerRepository _studentAnswerRepository;
    private readonly IMapper _mapper;
    public StudentAnswerService(IStudentAnswerRepository studentAnswerRepository, IMapper mapper)
    {
        _studentAnswerRepository = studentAnswerRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<StudentAnswerDto>>> AddRangeAsync(List<StudentAnswerCreateDto> studentAnswerCreateDtos)
    {
        var studentAnswers = new List<StudentAnswer>();
        foreach (var studentAnswerCreateDto in studentAnswerCreateDtos)
        {
            var hasStudentAnswer = await _studentAnswerRepository.AnyAsync(x => x.QuestionAnswerId == studentAnswerCreateDto.QuestionAnswerId && x.StudentQuestionId == studentAnswerCreateDto.StudentQuestionId);
            if (hasStudentAnswer)
            {
                return new ErrorDataResult<List<StudentAnswerDto>>(Messages.AddFailAlreadyExists);
            }
            var studentAnswer = _mapper.Map<StudentAnswer>(studentAnswerCreateDto);
            await _studentAnswerRepository.AddAsync(studentAnswer);
            studentAnswers.Add(studentAnswer);
        }
        await _studentAnswerRepository.SaveChangesAsync();
        return new SuccessDataResult<List<StudentAnswerDto>>(_mapper.Map<List<StudentAnswerDto>>(studentAnswers), Messages.AddSuccess);
    }

    public async Task<IDataResult<StudentAnswerDto>> AddAsync(StudentAnswerCreateDto studentAnswerCreateDto)
    {
        var hasStudentAnswer = await _studentAnswerRepository.AnyAsync(x => x.QuestionAnswerId == studentAnswerCreateDto.QuestionAnswerId && x.StudentQuestionId == studentAnswerCreateDto.StudentQuestionId);
        if (hasStudentAnswer)
        {
            return new ErrorDataResult<StudentAnswerDto>(Messages.AddFailAlreadyExists);
        }
        var studentAnswer = _mapper.Map<StudentAnswer>(studentAnswerCreateDto);
        await _studentAnswerRepository.AddAsync(studentAnswer);
        await _studentAnswerRepository.SaveChangesAsync();
        return new SuccessDataResult<StudentAnswerDto>(_mapper.Map<StudentAnswerDto>(studentAnswer), Messages.AddSuccess);
    }


    public async Task<IDataResult<List<StudentAnswerDto>>> GetByStudentQuestionId(Guid id)
    {
        var studentAnswers = await _studentAnswerRepository.GetAllAsync(x => x.StudentQuestionId == id);
        return new SuccessDataResult<List<StudentAnswerDto>>(_mapper.Map<List<StudentAnswerDto>>(studentAnswers), Messages.AddSuccess);
    }
}
