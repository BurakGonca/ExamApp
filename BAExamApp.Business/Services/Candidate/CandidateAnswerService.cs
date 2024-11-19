using BAExamApp.Entities.DbSets.Candidates;

namespace BAExamApp.Business.Services.Candidate
{
    public class CandidateAnswerService : ICandidateAnswerService
    {
        private readonly ICandidateAnswerRepository _candidateAnswerRepository;
        private readonly ICandidateQuestionRepository _candidateQuestionRepository;

        public CandidateAnswerService(ICandidateAnswerRepository candidateAnswerRepository, ICandidateQuestionRepository candidateQuestionRepository)
        {
            _candidateAnswerRepository = candidateAnswerRepository;
            _candidateQuestionRepository = candidateQuestionRepository;
        }

        public async Task<IDataResult<CandidateAnswerDto>> AddAsync(CandidateAnswerCreateDto candidateQuestionAnswerCreateDto)
        {
            try
            {
                var candidateQuestionAnswer = candidateQuestionAnswerCreateDto.Adapt<CandidateAnswer>();
                var candidateQuestionAnswerText = candidateQuestionAnswer.Answer;
                await _candidateAnswerRepository.AddAsync(candidateQuestionAnswer);
                await _candidateAnswerRepository.SaveChangesAsync();

                var candidateQuestionAnswerDto = candidateQuestionAnswer.Adapt<CandidateAnswerDto>();
                return new SuccessDataResult<CandidateAnswerDto>(candidateQuestionAnswerDto, Messages.CandidateAnswerAddedSuccess);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CandidateAnswerDto>(Messages.CandidateAnswerAddFailed);
            }
        }

        public async Task<IDataResult<List<CandidateAnswerDto>>> AddRangeAsync(List<CandidateAnswerCreateDto> questionAnswersCreateDto)
        {
            try
            {
                var candidateQuestionAnswers = questionAnswersCreateDto.Adapt<List<CandidateAnswer>>();
                await _candidateAnswerRepository.AddRangeAsync(candidateQuestionAnswers);
                await _candidateAnswerRepository.SaveChangesAsync();

                var candidateQuestionAnswerDtos = candidateQuestionAnswers.Adapt<List<CandidateAnswerDto>>();
                return new SuccessDataResult<List<CandidateAnswerDto>>(candidateQuestionAnswerDtos, Messages.CandidateAnswerAddedSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CandidateAnswerDto>>(Messages.CandidateAnswerAddFailed);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var candidateQuestionAnswer = await _candidateAnswerRepository.GetByIdAsync(id);
                if (candidateQuestionAnswer == null)
                {
                    return new ErrorResult(Messages.CandidateAnswerNotFound);
                }
                await _candidateAnswerRepository.DeleteAsync(candidateQuestionAnswer);
                await _candidateAnswerRepository.SaveChangesAsync();

                return new SuccessResult(Messages.CandidateAnswerDeletedSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.CandidateAnswerDeleteFailed);
            }
        }

        public async Task<IResult> DeleteRangeAsync(List<Guid> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var candidateQuestionAnswer = await _candidateAnswerRepository.GetByIdAsync(id);
                    if (candidateQuestionAnswer == null)
                    {
                        return new ErrorResult(Messages.CandidateAnswerNotFound);
                    }
                    await _candidateAnswerRepository.DeleteAsync(candidateQuestionAnswer);
                }

                await _candidateAnswerRepository.SaveChangesAsync();

                return new SuccessResult(Messages.CandidateAnswerDeletedSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.CandidateAnswerDeleteFailed);
            }
        }

        public async Task<IDataResult<CandidateAnswerDto>> GetById(Guid id, bool traking = true)
        {
            try
            {
                var candidateQuestionAnswer = await _candidateQuestionRepository.GetByIdAsync(id);
                if (candidateQuestionAnswer == null)
                {
                    return new ErrorDataResult<CandidateAnswerDto>(Messages.CandidateAnswerNotFound);
                }

                var candidateQuestionAnswerDto = candidateQuestionAnswer.Adapt<CandidateAnswerDto>();
                return new SuccessDataResult<CandidateAnswerDto>(candidateQuestionAnswerDto, Messages.CandidateAnswerFoundSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CandidateAnswerDto>(Messages.CandidateAnswerNotFound);
                throw;
            }
        }

        public async Task<IDataResult<List<CandidateAnswerDto>>> GetByQuestionId(Guid id)
        {
            try
            {
                var candidateQuestionAnswers = await _candidateAnswerRepository
                    .GetAllWithIncludeAsync(
                        x => x.QuestionId == id,
                        x => x.Question);

                if (candidateQuestionAnswers == null || !candidateQuestionAnswers.Any())
                {
                    return new ErrorDataResult<List<CandidateAnswerDto>>(Messages.CandidateAnswerNotFound);
                }

                var candidateQuestionAnswerList = candidateQuestionAnswers.ToList();

                var candidateQuestionAnswerDtos = candidateQuestionAnswerList.Adapt<List<CandidateAnswerDto>>();
                return new SuccessDataResult<List<CandidateAnswerDto>>(candidateQuestionAnswerDtos, Messages.CandidateAnswerFoundSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CandidateAnswerDto>>(Messages.CandidateAnswerNotFound);
            }
        }

        public async Task<IDataResult<CandidateAnswerDto>> UpdateAsync(CandidateAnswerUpdateDto candidateQuestionAnswersUpdateDto)
        {
            try
            {
                var existingEntity = await _candidateAnswerRepository.GetByIdAsync(candidateQuestionAnswersUpdateDto.Id);
                if (existingEntity == null)
                {
                    return new ErrorDataResult<CandidateAnswerDto>(Messages.CandidateAnswerNotFound);

                }
                var updatedEntity = candidateQuestionAnswersUpdateDto.Adapt(existingEntity);
                await _candidateAnswerRepository.UpdateAsync(updatedEntity);
                await _candidateAnswerRepository.SaveChangesAsync();

                return new SuccessDataResult<CandidateAnswerDto>(updatedEntity.Adapt<CandidateAnswerDto>(),Messages.CandidateAnswerUpdatedSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CandidateAnswerDto>(Messages.CandidateAnswerUpdatedFailed);
            }
        }

        public async Task<IDataResult<List<CandidateAnswerDto>>> UpdateRangeAsync(List<CandidateAnswerCreateDto> candidateQuestionAnswersCreateDto)
        {
            try
            {
                var CurrentQuestionAnswers = await _candidateAnswerRepository.GetAllAsync(x => x.QuestionId == candidateQuestionAnswersCreateDto[0].QuestionId);
                await DeleteRangeAsync(CurrentQuestionAnswers.Select(x => x.Id).ToList());

                return await AddRangeAsync(candidateQuestionAnswersCreateDto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CandidateAnswerDto>>(Messages.CandidateAnswerUpdatedFailed);
            }
        }
    }
}
