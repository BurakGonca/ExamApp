using BAExamApp.Business.Interfaces.Services;
using BAExamApp.Dtos.ExamRuleSubtopics;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Dtos.StudentQuestions;
using BAExamApp.Entities.Enums;
using System.Linq.Expressions;

namespace BAExamApp.Business.Services;
public class StudentQuestionService : IStudentQuestionService
{
    private readonly IStudentQuestionRepository _studentQuestionRepository;
    private readonly IMapper _mapper;
    private readonly IStudentExamService _studentExamService;
    private readonly IExamService _examService;
    private readonly IExamRuleService _examRuleService;
    private readonly IQuestionService _questionService;
    private readonly IQuestionDifficultyService _questionDifficultyService;
    private readonly IQuestionAnswerService _questionAnswerService;

    public StudentQuestionService(IStudentQuestionRepository studentQuestionRepository, IMapper mapper, IStudentExamService studentExamService, IExamService examService, IExamRuleService examRuleService, IQuestionService questionService, IQuestionDifficultyService questionDifficultyService, IQuestionAnswerService questionAnswerService)
    {
        _studentQuestionRepository = studentQuestionRepository;
        _mapper = mapper;
        _studentExamService = studentExamService;
        _examService = examService;
        _examRuleService = examRuleService;
        _questionService = questionService;
        _questionDifficultyService = questionDifficultyService;
        _questionAnswerService = questionAnswerService;
    }

    public async Task<IDataResult<StudentQuestionDto>> GetByIdAsync(Guid id)
    {
        var studentQuestion = await _studentQuestionRepository.GetByIdAsync(id);
        if (studentQuestion == null)
        {
            return new ErrorDataResult<StudentQuestionDto>(Messages.StudentQuestionNotFound);
        }

        return new SuccessDataResult<StudentQuestionDto>(_mapper.Map<StudentQuestionDto>(studentQuestion), Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<StudentQuestionListDto>>> GetByStudentExamIdAsync(Guid id)
    {
        var studentQuestion = await _studentQuestionRepository.GetAllAsync(x => x.StudentExamId == id);

        if (studentQuestion == null)
        {
            return new ErrorDataResult<List<StudentQuestionListDto>>(Messages.QuestionNotFound);
        }

        return new SuccessDataResult<List<StudentQuestionListDto>>(_mapper.Map<List<StudentQuestionListDto>>(studentQuestion), Messages.AddSuccess);
    }
    public async Task<IDataResult<List<StudentQuestionDto>>> GetAllAsync(Expression<Func<StudentQuestion, bool>> predicate = null)
    {
        var studentQuestions = await _studentQuestionRepository.GetAllAsync(predicate);

        if (studentQuestions == null)
        {
            return new ErrorDataResult<List<StudentQuestionDto>>(Messages.QuestionNotFound);
        }

        return new SuccessDataResult<List<StudentQuestionDto>>(_mapper.Map<List<StudentQuestionDto>>(studentQuestions), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<StudentQuestionDto>>> AddRangeAsync(List<StudentQuestionCreateDto> studentQuestionsCreateDto)
    {
        var studentQuestions = new List<StudentQuestion>();

        foreach (var studentQuestionCreateDto in studentQuestionsCreateDto)
        {
            var hasStudentQuestion = await _studentQuestionRepository.AnyAsync(x => x.StudentExamId == studentQuestionCreateDto.StudentExamId && x.QuestionId == studentQuestionCreateDto.QuestionId);

            if (hasStudentQuestion)
            {
                return new ErrorDataResult<List<StudentQuestionDto>>(Messages.AddFailAlreadyExists);
            }

            var classroomProduct = _mapper.Map<StudentQuestion>(studentQuestionCreateDto);

            await _studentQuestionRepository.AddAsync(classroomProduct);

            studentQuestions.Add(classroomProduct);
        }
        await _studentQuestionRepository.SaveChangesAsync();

        return new SuccessDataResult<List<StudentQuestionDto>>(_mapper.Map<List<StudentQuestionDto>>(studentQuestions), Messages.AddSuccess);
    }

    public async Task<IDataResult<List<StudentQuestionDto>>> AddRangeForExamIdAsync(Guid id)
    {
        Random rnd = new Random();

        var studentExams = await _studentExamService.GetAllByExamIdAsync(id);
        var exam = (await _examService.GetByIdAsync(id)).Data;
        var examRule = (await _examRuleService.GetByIdAsync(exam.ExamRuleId)).Data;

        var studentQuestions = new List<StudentQuestion>();

        var maxScorePerDifficultyCoefficient = await CalculateScorePerDifficultyCoefficient(examRule.ExamRuleSubtopics, (double)(exam.MaxScore));
        var bonusScorePerDifficultyCoefficient = await CalculateScorePerDifficultyCoefficient(examRule.ExamRuleSubtopics, (double)(exam.BonusScore));

        switch (exam.ExamCreationType)
        {
            case ExamCreationType.SameForEveryone:
                {
                    var questionListResult = await CreateQuestionPoolForExamRuleSubtopicsAsync(examRule.ExamRuleSubtopics);

                    if (!questionListResult.IsSuccess)
                        return new ErrorDataResult<List<StudentQuestionDto>>(Messages.PleaseAddQuestionsBefore);

                    foreach (var studentExam in studentExams.Data)
                    {
                        int questionOrder = 1;

                        foreach (var question in questionListResult.Data)
                        {
                            var questionOfStudent = new StudentQuestion();

                            questionOfStudent.QuestionOrder = questionOrder;
                            questionOfStudent.QuestionId = question.Id;
                            questionOfStudent.StudentExamId = studentExam.Id;

                            var questionDifficulty = await _questionDifficultyService.GetDetailsByIdAsync(question.QuestionDifficultyId);
                            if (questionDifficulty != null)
                            {
                                questionOfStudent.MaxScore = (int)(Math.Round(questionDifficulty.Data.ScoreCoefficient * maxScorePerDifficultyCoefficient));
                                questionOfStudent.BonusScore = (int)(Math.Round(questionDifficulty.Data.ScoreCoefficient * bonusScorePerDifficultyCoefficient));
                            }

                            studentQuestions.Add(questionOfStudent);
                            questionOrder++;
                        }
                    }
                }
                break;
            case ExamCreationType.SameQuestionsDifferentOrder:
                {
                    var questionListResult = await CreateQuestionPoolForExamRuleSubtopicsAsync(examRule.ExamRuleSubtopics);

                    if (!questionListResult.IsSuccess)
                        return new ErrorDataResult<List<StudentQuestionDto>>(Messages.PleaseAddQuestionsBefore);

                    foreach (var studentExam in studentExams.Data)
                    {
                        int questionOrder = 1;

                        for (int i = 0; i < questionListResult.Data.Count; i++)
                        {
                            var question = questionListResult.Data[rnd.Next(questionListResult.Data.Count)];

                            questionListResult.Data.Remove(question);
                            questionListResult.Data.Add(question);
                        }

                        foreach (var question in questionListResult.Data)
                        {
                            var questionOfStudent = new StudentQuestion();

                            questionOfStudent.QuestionOrder = questionOrder;
                            questionOfStudent.QuestionId = question.Id;
                            questionOfStudent.StudentExamId = studentExam.Id;

                            var questionDifficulty = await _questionDifficultyService.GetDetailsByIdAsync(question.QuestionDifficultyId);
                            if (questionDifficulty != null)
                            {
                                questionOfStudent.MaxScore = (int)(Math.Round(questionDifficulty.Data.ScoreCoefficient * maxScorePerDifficultyCoefficient));
                                questionOfStudent.BonusScore = (int)(Math.Round(questionDifficulty.Data.ScoreCoefficient * bonusScorePerDifficultyCoefficient));
                            }

                            studentQuestions.Add(questionOfStudent);
                            questionOrder++;
                        }
                    }
                }
                break;
            case ExamCreationType.DifferentQuestions:
                {
                    foreach (var studentExam in studentExams.Data)
                    {
                        int questionOrder = 1;

                        var questionListResult = await CreateQuestionPoolForExamRuleSubtopicsAsync(examRule.ExamRuleSubtopics);

                        if (!questionListResult.IsSuccess)
                            return new ErrorDataResult<List<StudentQuestionDto>>(Messages.PleaseAddQuestionsBefore);

                        foreach (var question in questionListResult.Data)
                        {
                            var questionOfStudent = new StudentQuestion();

                            questionOfStudent.QuestionOrder = questionOrder;
                            questionOfStudent.QuestionId = question.Id;
                            questionOfStudent.StudentExamId = studentExam.Id;

                            var questionDifficulty = await _questionDifficultyService.GetDetailsByIdAsync(question.QuestionDifficultyId);
                            if (questionDifficulty != null)
                            {
                                questionOfStudent.MaxScore = (int)(Math.Round(questionDifficulty.Data.ScoreCoefficient * maxScorePerDifficultyCoefficient));
                                questionOfStudent.BonusScore = (int)(Math.Round(questionDifficulty.Data.ScoreCoefficient * bonusScorePerDifficultyCoefficient));
                            }

                            studentQuestions.Add(questionOfStudent);
                            questionOrder++;
                        }
                    }
                }
                break;
            default:
                break;
        }

        var maxScoreDifference = await CalculateTotalScoreDifferenceWithMaxAvailableScore(examRule.ExamRuleSubtopics, (double)(exam.MaxScore));

        if (maxScoreDifference != 0)
        {
            var count = 0;
            var questionCountPerStudent = (studentQuestions.Count) / (studentExams.Data.Count);
            foreach (var studentExam in studentExams.Data)
            {
                for (var i = 0; i < Math.Abs(maxScoreDifference); i++)
                {
                    studentQuestions[rnd.Next(count * questionCountPerStudent, (count + 1) * questionCountPerStudent)].MaxScore += (int)(maxScoreDifference / Math.Abs(maxScoreDifference));
                }
                count++;
            }
        }

        var bonusScoreDifference = await CalculateTotalScoreDifferenceWithMaxAvailableScore(examRule.ExamRuleSubtopics, (double)(exam.BonusScore));

        if (bonusScoreDifference != 0)
        {
            var count = 0;
            var questionCountPerStudent = (studentQuestions.Count) / (studentExams.Data.Count);
            foreach (var studentExam in studentExams.Data)
            {
                for (var i = 0; i < Math.Abs(bonusScoreDifference); i++)
                {
                    studentQuestions[rnd.Next(count * questionCountPerStudent, (count + 1) * questionCountPerStudent)].BonusScore += (int)(bonusScoreDifference / Math.Abs(bonusScoreDifference));
                }
                count++;
            }
        }

        await _studentQuestionRepository.AddRangeAsync(studentQuestions);
        await _studentQuestionRepository.SaveChangesAsync();

        return new SuccessDataResult<List<StudentQuestionDto>>(_mapper.Map<List<StudentQuestionDto>>(studentQuestions), Messages.AddSuccess);
    }

    public async Task<IDataResult<List<QuestionListDto>>> CreateQuestionPoolForExamRuleSubtopicsAsync(List<ExamRuleSubtopicDto> examRuleSubtopics)
    {
        Random rnd = new Random();

        var questionList = new List<QuestionListDto>();

        foreach (var examRuleSubtopic in examRuleSubtopics)
        {
            //List<QuestionListDto> questionsFiltered = (await _questionService.GetAllByExamRuleSubtopicAsync(examRuleSubtopic.QuestionDifficultyId, examRuleSubtopic.QuestionType, examRuleSubtopic.SubtopicId)).Data;

            List<Guid> subtopicIds = new List<Guid> { examRuleSubtopic.SubtopicId };
            List<QuestionListDto> questionsFiltered = (await _questionService.GetAllByExamRuleSubtopicAsync(examRuleSubtopic.QuestionDifficultyId, examRuleSubtopic.QuestionType, subtopicIds)).Data;

            if (questionsFiltered.Count < examRuleSubtopic.QuestionCount)
            {
                return new ErrorDataResult<List<QuestionListDto>>(Messages.PleaseAddQuestionsBefore);
            }

            for (var i = 0; i < examRuleSubtopic.QuestionCount; i++)
            {
                var question = questionsFiltered[rnd.Next(questionsFiltered.Count)];

                questionsFiltered.Remove(question);

                questionList.Add(question);
            }
        }

        return new SuccessDataResult<List<QuestionListDto>>(questionList, Messages.AddSuccess);
    }

    public async Task<IDataResult<StudentQuestionDetailsDto>> GetByStudentExamIdAndQuestionOrderAsync(Guid id, int questionOrder)
    {
        var studentQuestion = await _studentQuestionRepository.GetAsync(x => x.StudentExamId == id && x.QuestionOrder == questionOrder);

        if (studentQuestion == null)
        {
            return new ErrorDataResult<StudentQuestionDetailsDto>(Messages.QuestionNotFound);
        }

        return new SuccessDataResult<StudentQuestionDetailsDto>(_mapper.Map<StudentQuestionDetailsDto>(studentQuestion), Messages.AddSuccess);
    }

    public async Task<IDataResult<StudentQuestionDto>> UpdateAsync(StudentQuestionUpdateDto studentQuestionUpdateDto)
    {
        var studentQuestion = await _studentQuestionRepository.GetByIdAsync(studentQuestionUpdateDto.Id);

        if (studentQuestion is null)
        {
            return new ErrorDataResult<StudentQuestionDto>(Messages.StudentExamNotFound);
        }

        var updatedStudentQuestion = _mapper.Map(studentQuestionUpdateDto, studentQuestion);

        await _studentQuestionRepository.UpdateAsync(updatedStudentQuestion);
        await _studentQuestionRepository.SaveChangesAsync();

        return new SuccessDataResult<StudentQuestionDto>(_mapper.Map<StudentQuestionDto>(updatedStudentQuestion), Messages.UpdateSuccess);
    }
    public async Task ScoringOfTheExam(Guid QuestionID)
    {
        var filteredQuestions = await _studentQuestionRepository.GetAllAsync(x => x.QuestionId == QuestionID);

        var distinctStudentExamIds = filteredQuestions
            .Select(x => x.StudentExam) // StudentExam nesnesine erişim
            .Where(se => se.Exam.ExamClassrooms.Any(ec => ec.Classroom.ClosedDate > DateTime.Today)) // Filtreleme işlemi
            .GroupBy(se => se.Id) // StudentExamId'ye göre gruplama
            .Where(group => group.Count() == 1) // Sadece bir kez bulunanları al
            .Select(group => group.Key); // Grupların anahtarlarını (StudentExamId) seç
        var distinctQuestions = filteredQuestions.Where(x => distinctStudentExamIds.Contains(x.StudentExamId)).ToList();

        foreach (var distinctQuestion in distinctQuestions)
        {
            var studentExam = await _studentExamService.GetByIdAsync(distinctQuestion.StudentExamId);
            var studentExamScore = await CalculateStudentQuestionScoresAndExamScore(studentExam.Data);
            var mappedExam = _mapper.Map<StudentExamUpdateDto>(studentExam.Data);
            mappedExam.Score = studentExamScore;
            await _studentExamService.UpdateAsync(mappedExam);
        }
    }
    private async Task<decimal?> CalculateStudentQuestionScoresAndExamScore(StudentExamDto studentExam)
    {
        var studentQuestionsResult = await GetByStudentExamIdAsync(studentExam.Id);
        studentExam.Score = 0;
        if (studentQuestionsResult.IsSuccess)
        {
            foreach (var item in studentQuestionsResult.Data)
            {
                var studentQuestionResult = await GetByIdAsync(item.Id);
                if (studentQuestionResult.IsSuccess)
                {
                    var studentQuestion = studentQuestionResult.Data;

                    studentQuestion.Score = 0;

                    if (studentQuestion.StudentAnswers.Count > 0)
                    {
                        var answerIsCorrect = true;
                        foreach (var studentAnswer in studentQuestion.StudentAnswers)
                        {
                            var questionAnswerResult = await _questionAnswerService.GetById(studentAnswer.QuestionAnswerId, false);

                            if (questionAnswerResult.IsSuccess)
                            {
                                answerIsCorrect = studentAnswer.IsSelected == questionAnswerResult.Data.IsRightAnswer ? answerIsCorrect : false;
                            }
                            else
                                answerIsCorrect = false;
                        }
                        studentQuestion.Score = answerIsCorrect ? studentQuestion.MaxScore + studentQuestion.BonusScore : studentQuestion.Score;
                        studentExam.Score += studentQuestion.Score;
                        var mappedQuestion = _mapper.Map<StudentQuestionUpdateDto>(studentQuestion);
                        await UpdateAsync(mappedQuestion);
                    }
                    else
                    {
                        studentExam.Score = null;
                    }
                }
            }
        }
        return studentExam.Score;
    }
    protected async Task<double> CalculateScorePerDifficultyCoefficient(List<ExamRuleSubtopicDto> examRuleSubtopics, double maxAvailableScore)
    {
        double totalDifficultyCoefficient = 0.0;

        if (examRuleSubtopics != null && examRuleSubtopics.Count > 0)
        {
            foreach (var examRuleSubtopic in examRuleSubtopics)
            {
                var questionDifficulty = await _questionDifficultyService.GetDetailsByIdAsync(examRuleSubtopic.QuestionDifficultyId);
                if (questionDifficulty != null)
                    totalDifficultyCoefficient += questionDifficulty.Data.ScoreCoefficient * examRuleSubtopic.QuestionCount;
            }
        }
        return maxAvailableScore / (totalDifficultyCoefficient > 0 ? totalDifficultyCoefficient : 1);
    }

    protected async Task<double> CalculateTotalScoreDifferenceWithMaxAvailableScore(List<ExamRuleSubtopicDto> examRuleSubtopics, double maxAvailableScore)
    {
        var scorePerDifficultyCoefficient = await CalculateScorePerDifficultyCoefficient(examRuleSubtopics, maxAvailableScore);

        var totalScore = 0.0;

        if (examRuleSubtopics != null && examRuleSubtopics.Count > 0)
        {
            foreach (var examRuleSubtopic in examRuleSubtopics)
            {
                var questionDifficulty = await _questionDifficultyService.GetDetailsByIdAsync(examRuleSubtopic.QuestionDifficultyId);
                if (questionDifficulty != null)
                    totalScore += Math.Round(questionDifficulty.Data.ScoreCoefficient * scorePerDifficultyCoefficient) * examRuleSubtopic.QuestionCount;
            }
        }
        return maxAvailableScore - totalScore;
    }
    public async Task<IDataResult<string>> TotalGivenTimeAsync(IEnumerable<ExamRuleSubtopicDto> subtopics)
    {
       
        TimeSpan totalTimeGiven = TimeSpan.Zero;

        foreach (var subtopic in subtopics)
        {
            
            var questionListResult = await CreateQuestionPoolForExamRuleSubtopicsAsync(new List<ExamRuleSubtopicDto> { subtopic });

            if (questionListResult == null || !questionListResult.IsSuccess)
            {
                return new ErrorDataResult<string>(string.Empty, Messages.QuestionPoolCreationFailed);
            }

            foreach (var question in questionListResult.Data)
            {
                totalTimeGiven += question.TimeGiven;
            }
        }

        
        if (totalTimeGiven.Seconds > 0)
        {
            totalTimeGiven = totalTimeGiven.Add(TimeSpan.FromMinutes(1)); 
        }

        
        return new SuccessDataResult<string>(totalTimeGiven.ToString(@"hh\:mm"), Messages.TotalTimeCalculated);
    }

}
