using BAExamApp.Dtos.TestExams;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Business.Services;
public class TestExamService : ITestExamService
{
    private readonly ITestExamRepository _testExamRepository;
    private readonly ITestExamQuestionRepository _testExamQuestionRepository;
    private readonly ITestExamTesterRepository _testExamsTesterTrainerRepository;
    private IMapper _mapper;

    public TestExamService(ITestExamRepository testExamRepository, IMapper mapper, ITestExamQuestionRepository testExamQuestionRepository, ITestExamTesterRepository testExamsTesterTrainerRepository)
    {
        _testExamRepository = testExamRepository;
        _mapper = mapper;
        _testExamQuestionRepository = testExamQuestionRepository;
        _testExamsTesterTrainerRepository = testExamsTesterTrainerRepository;
    }

    public async Task<IDataResult<TestExamDto>> CreateTestExamAsync(TestExamCreateDto examCreateDto)
    {
        var testExam = _mapper.Map<TestExam>(examCreateDto);
        testExam.State = State.Awaited;

        var testExamResult = await _testExamRepository.AddAsync(testExam);

        foreach (var questionId in examCreateDto.SelectedQuestionIds)
        {
            TestExamQuestion testQuestion = new TestExamQuestion()
            {
                QuestionId = questionId,
                TestExamId = testExamResult.Id
            };

            await _testExamQuestionRepository.AddAsync(testQuestion);
            testExam.TestExamQuestions.Add(testQuestion);
        }

        foreach (var trainerId in examCreateDto.SelectedTrainerIds)
        {
            TestExamTester testerTrainer = new TestExamTester()
            {
                TesterId = trainerId,
                TestExamId = testExamResult.Id
            };

            await _testExamsTesterTrainerRepository.AddAsync(testerTrainer);
            testExam.TestExamTesters.Add(testerTrainer);
        }

        await _testExamRepository.SaveChangesAsync();

        return new SuccessDataResult<TestExamDto>(_mapper.Map<TestExamDto>(testExam), Messages.AddSuccess);
    }
}
