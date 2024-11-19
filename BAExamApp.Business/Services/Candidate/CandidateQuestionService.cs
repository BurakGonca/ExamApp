using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.Entities.Enums;
using System.Linq.Expressions;

namespace BAExamApp.Business.Services.Candidate
{
    public class CandidateQuestionService : ICandidateQuestionService
    {
        private readonly ICandidateQuestionRepository _candidateQuestionRepository;
        private readonly ICandidateAnswerRepository _candidateAnswerRepository;
        private readonly ICandidateAnswerService _candidateAnswerService;
        private readonly ICandidateCandidateQuestionRepository _candidateCandidateQuestionRepository;
        private readonly ICandidateCandidateAnswerRepository _candidateCandidateAnswerRepository;

        public CandidateQuestionService(
            ICandidateQuestionRepository candidateQuestionRepository,
            ICandidateAnswerRepository candidateAnswerRepository, ICandidateAnswerService candidateAnswerService, ICandidateCandidateQuestionRepository candidateCandidateQuestionRepository, ICandidateCandidateAnswerRepository candidateCandidateAnswerRepository)
        {
            _candidateQuestionRepository = candidateQuestionRepository;
            _candidateAnswerRepository = candidateAnswerRepository;
            _candidateAnswerService = candidateAnswerService;
            _candidateCandidateQuestionRepository = candidateCandidateQuestionRepository;
            _candidateCandidateAnswerRepository = candidateCandidateAnswerRepository;
        }
        public async Task<IDataResult<CandidateQuestionDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var candidateQuestion = await _candidateQuestionRepository.GetByIdAsync(id);
                if (candidateQuestion == null)
                {
                    return new ErrorDataResult<CandidateQuestionDto>(Messages.QuestionNotFound);
                }

                var candidateQuestionDto = candidateQuestion.Adapt<CandidateQuestionDto>();
                return new SuccessDataResult<CandidateQuestionDto>(candidateQuestionDto, Messages.CandidateQuestionFoundSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CandidateQuestionDto>(Messages.CandidateQuestionNotFound);
            }
        }
        public async Task<IDataResult<List<CandidateQuestionListDto>>> GetAllAsync()
        {
            try
            {
                var candidateQuestionList = await _candidateQuestionRepository.GetAllAsync();
                var candidateQuestionListDto = candidateQuestionList.Adapt<List<CandidateQuestionListDto>>() ?? new List<CandidateQuestionListDto>();

                if (candidateQuestionListDto == null || candidateQuestionListDto.Count == 0)
                {
                    return new SuccessDataResult<List<CandidateQuestionListDto>>(candidateQuestionListDto, Messages.CandidateQuestionListEmpty);
                }

                return new SuccessDataResult<List<CandidateQuestionListDto>>(candidateQuestionListDto, Messages.CandidateQuestionListedSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CandidateQuestionListDto>>(new List<CandidateQuestionListDto>(), Messages.CandidateQuestionListFailed);
            }
        }

        public async Task<IDataResult<CandidateQuestionDto>> AddAsync(CandidateQuestionCreateDto candidateQuestionCreateDto)
        {
            try
            {
                var candidateQuestion = candidateQuestionCreateDto.Adapt<CandidateQuestion>();
                await _candidateQuestionRepository.AddAsync(candidateQuestion);
                await _candidateQuestionRepository.SaveChangesAsync();

                var candidateQuestionDto = candidateQuestion.Adapt<CandidateQuestionDto>();
                return new SuccessDataResult<CandidateQuestionDto>(candidateQuestionDto, Messages.CandidateQuestionAddedSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CandidateQuestionDto>(Messages.CandidateQuestionAddFailed);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var candidateQuestion = await _candidateQuestionRepository.GetByIdAsync(id);
                if (candidateQuestion == null)
                {
                    return new ErrorResult(Messages.QuestionNotFound);
                }
                // Soru herhangi bir sınavda kullanılıyor mu kontrol et
                // Exam repository oluşturulduktan sonra kontrol edilecek. Örenk değer ataması yapılmıştır.
                bool isQuestionUsedInExam = await _candidateCandidateQuestionRepository.AnyAsync(q => q.QuestionId == id);

                if (isQuestionUsedInExam)
                {
                    return new ErrorResult(Messages.QuestionUsedInExam);
                }
                else
                {
                    foreach (var answer in candidateQuestion.QuestionAnswers)
                    {
                        await _candidateAnswerRepository.DeleteAsync(answer);
                    }
                    await _candidateQuestionRepository.DeleteAsync(candidateQuestion);
                }

                await _candidateQuestionRepository.SaveChangesAsync();

                return new SuccessResult(Messages.CandidateQuestionDeletedSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.CandidateQuestionDeleteFailed);
            }
        }

        public async Task<IDataResult<CandidateQuestionDto>> UpdateAsync(CandidateQuestionUpdateDto candidateQuestionUpdateDto)
        {
            try
            {
                var candidateQuestion = await _candidateQuestionRepository.GetByIdAsync(candidateQuestionUpdateDto.Id);
                if (candidateQuestion == null)
                {
                    return new ErrorDataResult<CandidateQuestionDto>(Messages.CandidateQuestionNotFound);
                }

                candidateQuestion.Id = candidateQuestionUpdateDto.Id;
                candidateQuestion.Content = candidateQuestionUpdateDto.Content;
                candidateQuestion.CandidateQuestionType = (CandidateQuestionType)candidateQuestionUpdateDto.CandidateQuestionType;
                candidateQuestion.Image = candidateQuestionUpdateDto.Image;
                candidateQuestion.CandidateQuestionSubjectId = candidateQuestionUpdateDto.CandidateQuestionSubjectId;
                await _candidateQuestionRepository.UpdateAsync(candidateQuestion);
                await _candidateQuestionRepository.SaveChangesAsync();

                var candidateQuestionAnswersResult = await _candidateAnswerService.GetByQuestionId(candidateQuestion.Id);


                var updatedQuestionDto = candidateQuestion.Adapt<CandidateQuestionDto>();
                updatedQuestionDto.QuestionAnswers = candidateQuestionAnswersResult.Data;
                return new SuccessDataResult<CandidateQuestionDto>(updatedQuestionDto, Messages.CandidateQuestionUpdatedSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CandidateQuestionDto>(Messages.CandidateAnswerUpdatedFailed);
            }
        }

        public async Task<IDataResult<List<CandidateQuestionListDto>>> GetActiveQuestionsAsync()
        {
            try
            {
                var allQuestions = await _candidateQuestionRepository.GetAllAsync();
                var activeQuestionList = allQuestions.Where(x => x.Status == Core.Enums.Status.Active);

                var activeQuestionListDto = activeQuestionList.Adapt<List<CandidateQuestionListDto>>() ?? new List<CandidateQuestionListDto>();

                if (activeQuestionListDto == null || activeQuestionListDto.Count == 0)
                {
                    return new ErrorDataResult<List<CandidateQuestionListDto>>(Messages.CandidateQuestionListEmpty);
                }

                return new SuccessDataResult<List<CandidateQuestionListDto>>(activeQuestionListDto, Messages.CandidateQuestionListedSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CandidateQuestionListDto>>(new List<CandidateQuestionListDto>(), Messages.CandidateQuestionListFailed);
            }
        }

        public async Task<IDataResult<List<CandidateQuestionListDto>>> GetInactiveQuestionsAsync()
        {
            try
            {
                var allQuestions = await _candidateQuestionRepository.GetAllAsync();
                var inactiveQuestionList = allQuestions.Where(x => x.Status == Core.Enums.Status.Passive);

                var inactiveQuestionListDto = inactiveQuestionList.Adapt<List<CandidateQuestionListDto>>() ?? new List<CandidateQuestionListDto>();

                if (inactiveQuestionListDto == null || inactiveQuestionListDto.Count == 0)
                {
                    return new ErrorDataResult<List<CandidateQuestionListDto>>(Messages.CandidateQuestionListEmpty);
                }

                return new SuccessDataResult<List<CandidateQuestionListDto>>(inactiveQuestionListDto, Messages.CandidateQuestionListedSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CandidateQuestionListDto>>(new List<CandidateQuestionListDto>(), Messages.CandidateQuestionListFailed);
            }
        }


        public async Task<IDataResult<CandidateQuestionDetailsDto>> GetDetailsByIdAsync(Guid id)
        {
            try
            {
                var candidateQuestion = await _candidateQuestionRepository.GetByIdAsync(id);
                if (candidateQuestion == null)
                {
                    return new ErrorDataResult<CandidateQuestionDetailsDto>(Messages.CandidateQuestionNotFound);
                }

                // Yanıtları filtrele
                candidateQuestion.QuestionAnswers = candidateQuestion.QuestionAnswers
                    .Where(qa => qa.Status != (Core.Enums.Status)4)
                    .ToList();

                var candidateQuestionDetails = candidateQuestion.Adapt<CandidateQuestionDetailsDto>();
                return new SuccessDataResult<CandidateQuestionDetailsDto>(candidateQuestionDetails, Messages.CandidateQuestionFoundSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CandidateQuestionDetailsDto>($"{Messages.QuestionNotFound}: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<CandidateQuestionListDto>>> GetQuestionBySearchValues(string? content, int? candidateQuestionType, Guid? candidateQuestionSubject)
        {
            // Tüm aday sorularını getir
            var candidateQuestionList = await _candidateQuestionRepository.GetAllAsync();

            if (!(content == null && candidateQuestionType == null && candidateQuestionSubject == null))
            {
                // İçerik ile filtreleme
                if (!string.IsNullOrEmpty(content))
                {
                    candidateQuestionList = candidateQuestionList.Where(x => x.Content.ToLower().Contains(content.ToLower())).ToList();
                }

                // Tür ile filtreleme
                if (candidateQuestionType != null && candidateQuestionType != 0)
                {
                    candidateQuestionList = candidateQuestionList.Where(x => (int)x.CandidateQuestionType == candidateQuestionType).ToList();
                }

                // Konu ile filtreleme
                if (candidateQuestionSubject != null && candidateQuestionSubject != Guid.Empty)
                {
                    candidateQuestionList = candidateQuestionList.Where(x => (Guid)x.CandidateQuestionSubjectId == candidateQuestionSubject).ToList();
                }
            }

            // Sonuçları dön
            return candidateQuestionList.Any()
                ? new SuccessDataResult<List<CandidateQuestionListDto>>(candidateQuestionList.Adapt<List<CandidateQuestionListDto>>(), Messages.ListedSuccess)
                : new ErrorDataResult<List<CandidateQuestionListDto>>(Messages.CandidateQuestionNotFound);
        }

        public async Task<IDataResult<CandidateQuestionDto>> SetQuestionAndAnswersToActiveAsync(Guid id)
        {
            try
            {
                var candidateQuestion = await _candidateQuestionRepository.GetByIdAsync(id);
                if (candidateQuestion == null)
                {
                    return new ErrorDataResult<CandidateQuestionDto>(Messages.QuestionNotFound);
                }

                candidateQuestion.Status = Core.Enums.Status.Active;

                foreach (var answer in candidateQuestion.QuestionAnswers)
                {
                    answer.Status = Core.Enums.Status.Active;
                }

                await _candidateQuestionRepository.UpdateAsync(candidateQuestion);
                await _candidateQuestionRepository.SaveChangesAsync();

                var candidateQuestionDto = candidateQuestion.Adapt<CandidateQuestionDto>();

                return new SuccessDataResult<CandidateQuestionDto>(candidateQuestionDto, Messages.QuestionAndAnswersSetActiveSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CandidateQuestionDto>(Messages.QuestionAndAnswersSetActiveFailed);
            }
        }

        public async Task<IDataResult<CandidateQuestionDto>> SetQuestionAndAnswersToInactiveAsync(Guid id)
        {
            try
            {
                var candidateQuestion = await _candidateQuestionRepository.GetByIdAsync(id);
                if (candidateQuestion == null)
                {
                    return new ErrorDataResult<CandidateQuestionDto>(Messages.QuestionNotFound);
                }

                candidateQuestion.Status = Core.Enums.Status.Passive;

                foreach (var answer in candidateQuestion.QuestionAnswers)
                {
                    answer.Status = Core.Enums.Status.Passive;
                }

                await _candidateQuestionRepository.UpdateAsync(candidateQuestion);
                await _candidateQuestionRepository.SaveChangesAsync();

                var candidateQuestionDto = candidateQuestion.Adapt<CandidateQuestionDto>();

                return new SuccessDataResult<CandidateQuestionDto>(candidateQuestionDto, Messages.QuestionAndAnswersSetInactiveSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CandidateQuestionDto>(Messages.QuestionAndAnswersSetInactiveFailed);
            }
        }

        public async Task<IDataResult<CandidateQuestionDto>> GetByIdWithFilteredAnswersAsync(Guid id)
        {
            try
            {
                Expression<Func<CandidateQuestion, bool>> expression = cq => cq.Id == id;
                Expression<Func<CandidateQuestion, object>> include = cq => cq.QuestionAnswers;

                var candidateQuestion = await _candidateQuestionRepository.GetByIdWithIncludeAsync(expression, include);
                if (candidateQuestion == null)
                {

                    return new ErrorDataResult<CandidateQuestionDto>(Messages.QuestionNotFound);
                }


                candidateQuestion.QuestionAnswers = candidateQuestion.QuestionAnswers.Where(qa => qa.Status != (Core.Enums.Status)4).ToList();

                var candidateQuestionDto = candidateQuestion.Adapt<CandidateQuestionDto>();
                foreach (var candidateQuestionAnswer in candidateQuestionDto.QuestionAnswers)
                {
                    candidateQuestionAnswer.IsAnswerInUse = await _candidateCandidateAnswerRepository.AnyAsync(cca => cca.CandidateAnswerId == candidateQuestionAnswer.Id);
                }

                return new SuccessDataResult<CandidateQuestionDto>(candidateQuestionDto, Messages.CandidateQuestionFoundSuccess);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<CandidateQuestionDto>($"{Messages.CandidateQuestionNotFound}: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<CandidateQuestionDto>>> GetQuestionsBySubjectIdAsync(Guid subjectId)
        {
            try
            {
                var candidateQuestionList = await _candidateQuestionRepository.GetBySubjectIdAsync(subjectId);
                var candidateQuestionListDto = candidateQuestionList.Adapt<List<CandidateQuestionDto>>();
                return new SuccessDataResult<List<CandidateQuestionDto>>(candidateQuestionListDto, Messages.QuestionAndAnswersSetActiveFailed);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CandidateQuestionDto>>(Messages.QuestionAndAnswersSetActiveFailed);
            }
        }
    }

}

