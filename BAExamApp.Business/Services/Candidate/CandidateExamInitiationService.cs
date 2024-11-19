using BAExamApp.Dtos.Candidate.CandidateExamInitiation;
using BAExamApp.Entities.DbSets.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateExamInitiationService : ICandidateExamInitiationService
{
    private readonly ICandidateCandidateAnswerRepository _candidateCandidateAnswerRepository;
    private readonly ICandidateCandidatesExamsRepository _candidateCandidatesExamsRepository;
    private readonly ICandidateCandidateQuestionRepository _candidateCandidateQuestionRepository;
    private readonly ICandidateExamEvaluationService _examEvaluationService;
    private readonly ICandidateCandidateAnswerRepository _candidateAnswerRepository;

    public CandidateExamInitiationService(ICandidateCandidateAnswerRepository candidateCandidateAnswerRepository, ICandidateCandidatesExamsRepository candidateCandidatesExamsRepository, ICandidateCandidateQuestionRepository candidateCandidateQuestionRepository, ICandidateExamEvaluationService examEvaluationService, ICandidateCandidateAnswerRepository candidateAnswerRepository)
    {
        _candidateCandidateAnswerRepository = candidateCandidateAnswerRepository;
        _candidateCandidatesExamsRepository = candidateCandidatesExamsRepository;
        _candidateCandidateQuestionRepository = candidateCandidateQuestionRepository;
        _examEvaluationService = examEvaluationService;
        _candidateAnswerRepository = candidateAnswerRepository;
    }
    /// <summary>
    /// Sınav açılma ekranındaki bilgilerin getirilmesi için gerekli method
    /// </summary>
    /// <param name="candidateId"></param>
    /// <param name="examId"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateStartExamDetailsDto>> GetExamStartDetailsAsync(Guid candidateId, Guid examId)
    {
        var candidateExam = await _candidateCandidatesExamsRepository.GetAsync(x => x.CandidateExamId == examId && x.CandidateId == candidateId);

        if (candidateExam == null)
        {
            return new ErrorDataResult<CandidateStartExamDetailsDto>(Messages.CandidateExamsNotFound);
        }

        var dto = candidateExam.CandidateExam.Adapt<CandidateStartExamDetailsDto>();
        dto.ExamId = candidateExam.CandidateExamId;
        dto.ExamName = candidateExam.CandidateExam.Name;
        dto.CandidateId = candidateExam.CandidateId;
        dto.FullName = candidateExam.Candidate.FirstName + " " + candidateExam.Candidate.LastName;
        dto.QuestionCount = candidateExam.CandidateExam.AlgorithmQuestionCount + candidateExam.CandidateExam.TestQuestionCount + candidateExam.CandidateExam.ClassicQuestionCount;
        dto.IsExamStarted = candidateExam.IsExamStarted;
        dto.IsExamFinished = candidateExam.IsExamFinished;
        return new SuccessDataResult<CandidateStartExamDetailsDto>(dto, Messages.CandidateExamFoundSuccess);
    }


    /// <summary>
    /// Aday sınav ekranında sınavı başlattığında çalışan method
    /// </summary>
    /// <param name="candidateId"></param>
    /// <param name="examId"></param>
    /// <returns></returns>
    public async Task<IResult> StartExamAsync(Guid candidateId, Guid examId)
    {
        try
        {
            var candidateExam = await _candidateCandidatesExamsRepository.GetAsync(x => x.CandidateExamId == examId & x.CandidateId == candidateId);


            if (candidateExam == null)
            {
                return new ErrorResult(Messages.CandidateExamNotFound);
            }

            var candidateQuestions = (await _candidateCandidateQuestionRepository.GetAllAsync()).Where(x => x.CandidatesExamsId == candidateExam.Id);

            var answers = new List<CandidateCandidateAnswer>();

            foreach (var question in candidateQuestions)
            {
                answers.Add(new CandidateCandidateAnswer() { CandidateQuestionId = question.Id, CandidateQuestion = question });
            }

            await _candidateCandidateAnswerRepository.AddRangeAsync(answers);
            await _candidateCandidateAnswerRepository.SaveChangesAsync();

            candidateExam.IsExamStarted = true;
            await _candidateCandidatesExamsRepository.UpdateAsync(candidateExam);
            await _candidateCandidatesExamsRepository.SaveChangesAsync();


            foreach (var question in candidateQuestions)
            {
                question.CandidateAnswerId = answers.FirstOrDefault(x => x.CandidateQuestionId == question.Id).Id;
                await _candidateCandidateQuestionRepository.UpdateAsync(question);
            }
            await _candidateCandidateQuestionRepository.SaveChangesAsync();

            return new SuccessResult(Messages.CandidateAnswerAddedSuccess);
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.CandidateAnswerAddFailed);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="questionInOrderDto"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateExamGetQuestionInOrderDto>> GetQuestionInOrderAsync(CandidateExamGetQuestionInOrderDto questionInOrderDto)
    {
        try
        {
            var candidateExam = await _candidateCandidatesExamsRepository.GetAsync(x => x.CandidateExamId == questionInOrderDto.ExamId && x.CandidateId == questionInOrderDto.CandidateId);

            if (candidateExam == null)
            {
                return new ErrorDataResult<CandidateExamGetQuestionInOrderDto>(Messages.CandidateExamsNotFound);
            }

            var questions = await _candidateCandidateQuestionRepository.GetAllAsync(x => x.CandidatesExamsId == candidateExam.Id);

            if (questions == null || !questions.Any())
            {
                return new ErrorDataResult<CandidateExamGetQuestionInOrderDto>(Messages.CandidateQuestionNotFound);
            }


            var question = questions.ElementAt(questionInOrderDto.QuestionInOrder);

            var candidateAnswer = await _candidateCandidateAnswerRepository.GetAsync(x => x.CandidateQuestionId == question.Id);

            if (candidateAnswer == null)
            {
                return new ErrorDataResult<CandidateExamGetQuestionInOrderDto>(Messages.CandidateAnswerNotFound);
            }

            var updatedDto = question.Question.Adapt(questionInOrderDto);
            updatedDto.QuestionAnswers = updatedDto.QuestionAnswers.Where(answer => answer.Status != Core.Enums.Status.Deleted).ToList();
            updatedDto.CandidateQuestionId = question.Id;
            updatedDto = candidateExam.CandidateExam.Adapt(updatedDto);
            updatedDto = candidateAnswer.Adapt(updatedDto);
            updatedDto.QuestionCount = candidateExam.CandidateExam.TestQuestionCount + candidateExam.CandidateExam.AlgorithmQuestionCount + candidateExam.CandidateExam.ClassicQuestionCount;

            return new SuccessDataResult<CandidateExamGetQuestionInOrderDto>(updatedDto, Messages.CandidateQuestionFoundSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateExamGetQuestionInOrderDto>(questionInOrderDto, Messages.CandidateQuestionNotFound);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="answerQuestionDto"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateExamAnswerQuestionDto>> AnswerQuestionAsync(CandidateExamAnswerQuestionDto answerQuestionDto)
    {
        try
        {
            var candidateExam = await _candidateCandidatesExamsRepository.GetAsync(x => x.CandidateExamId == answerQuestionDto.ExamId && x.CandidateId == answerQuestionDto.CandidateId);

            if (candidateExam == null)
            {
                return new ErrorDataResult<CandidateExamAnswerQuestionDto>(Messages.CandidateExamsNotFound);
            }

            var candidateQuestion = await _candidateCandidateQuestionRepository.GetAsync(x => x.CandidatesExamsId == candidateExam.Id);

            if (candidateQuestion == null)
            {
                return new ErrorDataResult<CandidateExamAnswerQuestionDto>(Messages.CandidateQuestionNotFound);
            }

            var candidateAnswer = await _candidateCandidateAnswerRepository.GetAsync(x => x.CandidateQuestionId == answerQuestionDto.CandidateQuestionId);

            if (candidateAnswer == null)
            {
                return new ErrorDataResult<CandidateExamAnswerQuestionDto>(Messages.CandidateAnswerNotFound);
            }

            if (answerQuestionDto.CandidateQuestionType == Entities.Enums.CandidateQuestionType.Test)
            {
                candidateAnswer.CandidateAnswerId = answerQuestionDto.CandidateAnswerId;
            }
            else
            {
                candidateAnswer.Answer = answerQuestionDto.Answer;
            }

            await _candidateCandidateAnswerRepository.UpdateAsync(candidateAnswer);
            await _candidateCandidateAnswerRepository.SaveChangesAsync();
            return new SuccessDataResult<CandidateExamAnswerQuestionDto>(candidateAnswer.Adapt(answerQuestionDto), Messages.CandidateAnswerUpdatedSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateExamAnswerQuestionDto>(answerQuestionDto, Messages.CandidateAnswerUpdatedFailed);
        }
    }

    public async Task<IResult> FinishExamAsync(Guid candidateId, Guid examId)
    {
        try
        {
            var candidateExam = await _candidateCandidatesExamsRepository.GetAsync(x => x.CandidateExamId == examId && x.CandidateId == candidateId);
            if (candidateExam != null)
            {
                candidateExam.IsExamFinished = true;

                await _candidateCandidatesExamsRepository.UpdateAsync(candidateExam);
                await _candidateCandidatesExamsRepository.SaveChangesAsync();
                await _examEvaluationService.EvaluateAllTestAndClassicQuestionsAsync(examId, candidateId);
                return new SuccessResult(Messages.UpdateSuccess);
            }
            return new ErrorResult(Messages.CandidateExamsNotFound);
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.UpdateFail);
        }
    }


}
