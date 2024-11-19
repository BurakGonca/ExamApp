using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.Students;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAExamApp.Business.Services;

public class QuestionService : IQuestionService
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IQuestionRevisionRepository _questionRevisionRepository;
    private readonly IQuestionAnswerRepository _questionAnswerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;
    private readonly ITrainerService _trainerService;
    private readonly IStudentQuestionRepository _studentQuestionRepository;
    private readonly IStudentAnswerRepository studentAnswerRepository;
    private readonly IAdminService _adminService;


    public QuestionService(IQuestionRepository questionRepository, IMapper mapper, ITrainerService trainerService, ITrainerRepository trainerRepository, IQuestionRevisionRepository questionRevisionRepository, IQuestionAnswerRepository questionAnswerRepository, IStudentQuestionRepository studentQuestionRepository, IStudentAnswerRepository studentAnswerRepository, IAdminService adminService)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _trainerService = trainerService;
        _trainerRepository = trainerRepository;
        _questionRevisionRepository = questionRevisionRepository;
        _questionAnswerRepository = questionAnswerRepository;
        _studentQuestionRepository = studentQuestionRepository;
        this.studentAnswerRepository = studentAnswerRepository;
        _adminService = adminService;
    }
    public async Task<IDataResult<QuestionDto>> GetByIdAsync(Guid id)
    {
        var question = await _questionRepository.GetByIdAsync(id);
        question.QuestionSubtopics = question.QuestionSubtopics.Where(x => x.Status != Status.Deleted).ToList();

        if (question == null)
        {
            return new ErrorDataResult<QuestionDto>(Messages.QuestionNotFound);
        }

        var questionDto = _mapper.Map<QuestionDto>(question);

        return new SuccessDataResult<QuestionDto>(questionDto, Messages.QuestionFoundSuccess);
    }

    public async Task<IDataResult<List<QuestionListDto>>> GetAllAsync()
    {
        var questions = await _questionRepository.GetAllAsync();

        return new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(questions), Messages.ListedSuccess);
    }
    
    public async Task<IDataResult<List<QuestionListDto>>> GetAllByStateAsync(State state)
    {
        var questions = await _questionRepository.GetAllAsync(x => x.State == state && x.Status != Status.Passive);
        var filteredQuestions = questions.Select(q =>
        {
            q.QuestionSubtopics = q.QuestionSubtopics.Where(qs => qs.Status != Status.Deleted ).ToList();
            return q;
        }).ToList();
        return new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(filteredQuestions), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<QuestionListDto>>> GetQuestionBySearchValues(string? content, string? subject, string? subtopics, string? questionDifficulty, string? questionCreatedDate, State state)
    {
        int nullParamCount = new[] { content, subject, subtopics, questionDifficulty }.Count(param => param != null );
        var deneme = DateTime.Parse(questionCreatedDate).Year.ToString();

        if (DateTime.Parse( questionCreatedDate).Year.ToString() != "1")
        {
            nullParamCount++;
        }

        var questionsByContent = await _questionRepository.GetAllAsync(x => x.Content.Contains(content) && x.State == state);
        var questionsBySubject = await _questionRepository.GetAllAsync(x => x.Subject.Id.ToString().Contains(subject) && x.State == state);
        var questionsBySubtopic = await _questionRepository.GetAllAsync(x => x.QuestionSubtopics.Select(x => x.Subtopic.Id.ToString()).Contains(subtopics) && x.State == state);
        var questionsByDifficulty = await _questionRepository.GetAllAsync(x => x.QuestionDifficulty.Name.Contains(questionDifficulty) && x.State == state);
        var questionsByCreatedDate = await _questionRepository.GetAllAsync(x => EF.Functions.DateDiffDay(x.CreatedDate, DateTime.Parse(questionCreatedDate)) == 0 && x.State == state);

        var filteredQuestions = IntersectNonEmpty(nullParamCount, questionsByContent, questionsBySubject, questionsBySubtopic, questionsByDifficulty, questionsByCreatedDate);


        filteredQuestions = filteredQuestions.Select(q =>
        {
            q.QuestionSubtopics = q.QuestionSubtopics.Where(qs => qs.Status != Status.Deleted).ToList();
            return q;
        }).ToList();




        return filteredQuestions.Any()
            ? new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(filteredQuestions), Messages.ListedSuccess)
            : new ErrorDataResult<List<QuestionListDto>>(Messages.QuestionNotFound);
    }

    public async Task<IDataResult<List<QuestionListDto>>> GetQuestionBySearchValuesByTrainerId(string? content, string? subject, string? subtopics, string? questionDifficulty, string? questionCreatedDate, State state, string? trainerIdentityId, string? trainerId)
    {
        int nullParamCount = new[] { content, subject, subtopics, questionDifficulty }.Count(param => param != null);
        var deneme = DateTime.Parse(questionCreatedDate).Year.ToString();

        if (DateTime.Parse(questionCreatedDate).Year.ToString() != "1")
        {
            nullParamCount++;
        }

        var questionsByContent = await _questionRepository.GetAllAsync(x => x.Content.Contains(content) && x.State == state && x.CreatedBy == trainerIdentityId);
        var questionsBySubject = await _questionRepository.GetAllAsync(x => x.Subject.Id.ToString().Contains(subject) && x.State == state && x.CreatedBy == trainerIdentityId);
        var questionsBySubtopic = await _questionRepository.GetAllAsync(x => x.QuestionSubtopics.Select(x => x.Subtopic.Id.ToString()).Contains(subtopics) && x.State == state && x.CreatedBy == trainerIdentityId);
        var questionsByDifficulty = await _questionRepository.GetAllAsync(x => x.QuestionDifficulty.Id.ToString().Contains(questionDifficulty) && x.State == state && x.CreatedBy == trainerIdentityId);
        var questionsByCreatedDate = await _questionRepository.GetAllAsync(x => EF.Functions.DateDiffDay(x.CreatedDate, DateTime.Parse(questionCreatedDate)) == 0 && x.State == state && x.CreatedBy == trainerIdentityId);

        var filteredQuestions = IntersectNonEmpty(nullParamCount, questionsByContent, questionsBySubject, questionsBySubtopic, questionsByDifficulty, questionsByCreatedDate);


        filteredQuestions = filteredQuestions.Select(q =>
        {
            q.QuestionSubtopics = q.QuestionSubtopics.Where(qs => qs.Status != Status.Deleted).ToList();
            return q;
        }).ToList();


        return filteredQuestions.Any()
            ? new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(filteredQuestions), Messages.ListedSuccess)
            : new ErrorDataResult<List<QuestionListDto>>(Messages.QuestionNotFound);
    }

    private static IEnumerable<T> IntersectNonEmpty<T>(int _nullParamCount, params IEnumerable<T>[] lists)
    {
        var nonEmptyLists = lists.Where(list => list != null && list.Any()).ToList();

        if (nonEmptyLists.Count == 0)
        {
            return new List<T>();
        }

        IEnumerable<T> result = nonEmptyLists[0];
        if (_nullParamCount == nonEmptyLists.Count)
        {


            for (int i = 1; i < nonEmptyLists.Count; i++)
            {
                result = result.Intersect(nonEmptyLists[i]).ToList();

                if (!result.Any())
                {
                    break;
                }
            }
            return result;
        }
        else
        {

            return result = new List<T>();
        }


    }

    public async Task<IDataResult<List<QuestionListDto>>> GetAllByStateAndTrainerIdAsync(string trainerIdentityId, string trainerId, State state)
    {
        var questionList = new List<Question>();

        if (state == State.Reviewed)
        {
            var result = await _questionRevisionRepository.GetAllAsync(qr =>
                qr.RequestedTrainerId.ToString().ToLower().Equals(trainerId) && qr.Status == Status.Active);

            foreach (var questionRevision in result)
            {
                var question = await _questionRepository.GetByIdAsync(questionRevision.QuestionId);
                if (question is null)
                {
                    continue;
                }
                question.QuestionSubtopics = question.QuestionSubtopics.Where(x => x.Status != Status.Deleted).ToList();
                questionList.Add(question!);
            }
        }
        else
        {
            var questions = await _questionRepository.GetAllAsync(question =>
                question.CreatedBy == trainerIdentityId &&
                question.State == state &&
                question.Status != Status.Deleted);

            foreach (var question in questions)
            {
                var nonDeletedSubtopics = question.QuestionSubtopics.Where(subtopic => subtopic.Status != Status.Deleted).ToList();
                question.QuestionSubtopics = nonDeletedSubtopics;

                if (nonDeletedSubtopics.Any())
                {
                    questionList.Add(question);
                }
            }
        }

        return new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(questionList), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<QuestionListDto>>> GetAllByFilterAsync(QuestionFilterDto questionFilterDto)
    {
        var expressionList = new List<Expression<Func<Question, bool>>>
        {
            x => x.State == State.Approved
        };

        if (questionFilterDto.QuestionDifficultyId != null)
            expressionList.Add(x => x.QuestionDifficultyId == questionFilterDto.QuestionDifficultyId);

        if (questionFilterDto.SubtopicId != null)
            expressionList.Add(x => x.QuestionSubtopics.Select(x => x.SubtopicId).FirstOrDefault() == questionFilterDto.SubtopicId);

        var firstExpression = expressionList[0];

        var body = firstExpression.Body;
        var parameters = firstExpression.Parameters.ToArray();

        foreach (var expression in expressionList.Skip(1))
        {
            var nextBody = Expression.Invoke(expression, parameters);
            body = Expression.AndAlso(body, nextBody);
        }

        var finalExpression = Expression.Lambda<Func<Question, bool>>(body, parameters);

        var questions = await _questionRepository.GetAllAsync(finalExpression);

        return new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(questions), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<QuestionListDto>>> GetAllByExamRuleSubtopicAsync(Guid questionDifficultyId, int questionType, List<Guid> subtopicId)
    {
        var questions = await _questionRepository.GetAllAsync(x => x.QuestionDifficultyId == questionDifficultyId && (int)x.QuestionType == questionType && x.QuestionSubtopics.Any(qs => subtopicId.Contains(qs.SubtopicId)) && x.State == State.Approved && x.Status != Status.Passive, true);

        return new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(questions), Messages.ListedSuccess);
    }

    public async Task<IDataResult<QuestionDetailsDto>> GetDetailsByIdAsync(Guid id)
    {
        var question = await _questionRepository.GetByIdAsync(id); 
        question.QuestionSubtopics = question.QuestionSubtopics.Where(x => x.Status != Status.Deleted).ToList();

        if (question is null)
        {
            return new ErrorDataResult<QuestionDetailsDto>(Messages.QuestionNotFound);
        }

        return new SuccessDataResult<QuestionDetailsDto>(_mapper.Map<QuestionDetailsDto>(question), Messages.FoundSuccess);
    }

    public async Task<IDataResult<QuestionDetailsForAdminDto>> GetDetailsByIdForAdminAsync(Guid id)
    {
        // Revize detayları için kullanılan metot
        var question = await _questionRepository.GetByIdWithIncludeRevisionAsync(id);
       

        question.QuestionSubtopics = question.QuestionSubtopics.Where(x => x.Status != Status.Deleted).ToList();

        if (question is null)
        {
            return new ErrorDataResult<QuestionDetailsForAdminDto>(Messages.QuestionNotFound);
        }
        var subject = question.Subject;
        var trainer = await _trainerService.GetByIdentityIdAsync(question.CreatedBy);
        var admin = await _adminService.GetByIdentityIdAsync(question.CreatedBy);
        question.CreatedBy = trainer.IsSuccess? trainer.Data.FirstName + " " + trainer.Data.LastName:admin.IsSuccess? admin.Data.FirstName + " " + admin.Data.LastName: string.Empty;




        var questionDetailsForAdminDto = _mapper.Map<QuestionDetailsForAdminDto>(question);
        questionDetailsForAdminDto.SubjectName = subject?.Name ?? " ";
        var studentsQuestions = await _studentQuestionRepository.GetAllAsync(x => x.QuestionId == id);

        var studentAnswers = await _questionAnswerRepository.GetAllAsync(x=> x.QuestionId == id);

        var examCount = 0;
        var correctCount = await _questionRepository.CorrectQuestionCount(id);
        var incorrectCount= await _questionRepository.IncorrectQuestionCount(id);
        var blankCount= await _questionRepository.EmptyQuestionCount(id);

      

        if (studentsQuestions is not null && studentsQuestions.Any())
        {
             examCount = studentsQuestions
                .GroupBy(sq => sq.StudentExamId)
                .Count();
            TimeSpan totalAnswerTimeTicks = TimeSpan.Zero;
            int totalAnswerTimeCount = 0;
            TimeSpan? maxTime = TimeSpan.MinValue;
            TimeSpan? minTime = TimeSpan.MaxValue;

            foreach (var studentQuestion in studentsQuestions)
            {
                if (studentQuestion.TimeFinished.HasValue && studentQuestion.TimeStarted.HasValue)
                {
                    TimeSpan? answerTime = studentQuestion.TimeFinished.Value - studentQuestion.TimeStarted.Value;
                    totalAnswerTimeTicks += answerTime ?? TimeSpan.Zero;
                    totalAnswerTimeCount++;
                    answerTime = TimeSpan.FromSeconds(Math.Round(answerTime.Value.TotalSeconds));
                    if (answerTime > maxTime)
                    {
                        maxTime = answerTime ?? TimeSpan.Zero;
                    }

                    if (answerTime < minTime)
                    {
                        minTime = answerTime ?? TimeSpan.Zero;
                    }
                }
            }
            if(totalAnswerTimeCount != 0)
            {
                TimeSpan? averageTicks = totalAnswerTimeTicks / totalAnswerTimeCount;
                questionDetailsForAdminDto.AverageAnswerTime = TimeSpan.FromSeconds(Math.Round(averageTicks.Value.TotalSeconds));
                questionDetailsForAdminDto.LongestAnswerTime = TimeSpan.FromSeconds(Math.Round(maxTime.Value.TotalSeconds));
                questionDetailsForAdminDto.ShortestAnswerTime = TimeSpan.FromSeconds(Math.Round(minTime.Value.TotalSeconds));
            }
      
            questionDetailsForAdminDto.RightAnswerCount= correctCount;
            questionDetailsForAdminDto.WrongAnswerCount = incorrectCount;
            questionDetailsForAdminDto.EmptyAnswerCount=blankCount;
            questionDetailsForAdminDto.TimesQuestionUsedInExam = examCount;
        }

        return new SuccessDataResult<QuestionDetailsForAdminDto>(questionDetailsForAdminDto, Messages.FoundSuccess);
    }

    public async Task<IDataResult<QuestionDto>> AddAsync(QuestionCreateDto questionCreateDto)
    {
        var question = _mapper.Map<Question>(questionCreateDto);

        await _questionRepository.AddAsync(question);
        await _questionRepository.SaveChangesAsync();

        return new SuccessDataResult<QuestionDto>(_mapper.Map<QuestionDto>(question), Messages.AddSuccess);
    }

    public async Task<IDataResult<QuestionDto>> UpdateAsync(QuestionUpdateDto questionUpdateDto)
    {
        var question = await _questionRepository.GetByIdAsync(questionUpdateDto.Id);
        var questionAnswer = await _questionAnswerRepository.GetByIdAsync(question.QuestionAnswers.First().Id);
        await _questionAnswerRepository.UpdateAsync(questionAnswer);


        if (question is null)
            return new ErrorDataResult<QuestionDto>(Messages.QuestionNotFound);

        var updatedQuestion = _mapper.Map(questionUpdateDto, question);

        await _questionRepository.UpdateAsync(updatedQuestion);


        return new SuccessDataResult<QuestionDto>(_mapper.Map<QuestionDto>(updatedQuestion), Messages.UpdateSuccess);
    }

    public async Task<IResult> UpdateStateAsync(Guid id, State state)
    {
        var question = await _questionRepository.GetByIdAsync(id);

        if (question is null)
        {
            return new ErrorDataResult<QuestionDetailsForAdminDto>(Messages.QuestionNotFound);
        }
        question.State = state;


        await _questionRepository.UpdateAsync(question);
        await _questionRepository.SaveChangesAsync();

        return new SuccessDataResult<QuestionDto>(_mapper.Map<QuestionDto>(question), Messages.UpdateSuccess);
    }
    public async Task<IResult> UpdateStateWithCommentAsync(Guid id, State state, string? rejectComment)
    {
        var question = await _questionRepository.GetByIdAsync(id);

        if (question is null)
        {
            return new ErrorDataResult<QuestionDetailsForAdminDto>(Messages.QuestionNotFound);
        }
        question.State = state;
        question.RejectComment = rejectComment;

        await _questionRepository.UpdateAsync(question);
        await _questionRepository.SaveChangesAsync();

        return new SuccessDataResult<QuestionDto>(_mapper.Map<QuestionDto>(question), Messages.UpdateSuccess);
    }


    public async Task<IResult> DeleteByStatusAsync(Guid questionId)
    {
        try
        {
             await _questionRepository.DeleteQuestionByStatus(questionId);
            return new SuccessResult(Messages.DeleteSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
        
    }

    public async Task<IResult> SetIsActive(Guid id, bool isActive)
    {
        var question = await _questionRepository.GetByIdAsync(id);

        if (question == null)
        {
            return new ErrorResult(Messages.QuestionNotFound);
        }

        question.IsActive = isActive;
        await _questionRepository.SaveChangesAsync();

        return question.IsActive switch
        {
            true => new SuccessResult(Messages.SetIsActiveTrue),
            false => new SuccessResult(Messages.SetIsActiveFalse)
        };
    }

    public async Task<bool> IsRevisionWanted(Guid questionId)
    {
        var question = await _questionRepository.GetByIdAsync(questionId);
        if (question.QuestionRevisions.Count>0)
        {
            return true;
        }
        return false;
    }

    public async Task<IDataResult<List<QuestionListDto>>> GetQuestionBySubtopicId(Guid subtopicId)
    {
        var filteredQuestions = await _questionRepository
            .GetAllAsync(q => q.Status == Status.Active
                           && q.State == State.Approved
                           && q.QuestionSubtopics.Any(qs => qs.SubtopicId == subtopicId));

        return filteredQuestions.Any()
            ? new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(filteredQuestions), Messages.ListedSuccess)
            : new ErrorDataResult<List<QuestionListDto>>(Messages.QuestionNotFound);
    }

}