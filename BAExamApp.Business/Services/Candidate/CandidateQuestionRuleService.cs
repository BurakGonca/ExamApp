using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateExamRule;
using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateQuestionRuleService : ICandidateQuestionRuleService
{
    private readonly IMapper _mapper;
    private readonly ICandidateQuestionRuleRepository _candidateQuestionRuleRepository;

    public CandidateQuestionRuleService(ICandidateQuestionRuleRepository candidateQuestionRuleRepository)
    {
        _candidateQuestionRuleRepository = candidateQuestionRuleRepository;
    }

    public async Task<IDataResult<CandidateQuestionRuleDto>> AddAsync(CandidateQuestionRuleCreateDto candidateQuestionRuleCreateDto)
    {
        var questionRule = candidateQuestionRuleCreateDto.Adapt<CandidateQuestionRule>();

        await _candidateQuestionRuleRepository.AddAsync(questionRule);
        await _candidateQuestionRuleRepository.SaveChangesAsync();

        var questionRuleToDto = questionRule.Adapt<CandidateQuestionRuleDto>();
        return new SuccessDataResult<CandidateQuestionRuleDto>(questionRuleToDto, Messages.AddSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var questionRule = await _candidateQuestionRuleRepository.GetByIdAsync(id);

        if (questionRule is null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }


        questionRule.Status = Core.Enums.Status.Deleted;

        await _candidateQuestionRuleRepository.DeleteAsync(questionRule);
        await _candidateQuestionRuleRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<List<CandidateQuestionRuleDto>>> GetAllAsync()
    {
        var questionRule = await _candidateQuestionRuleRepository.GetAllAsync(false);

        var toDto = questionRule.Adapt<List<CandidateQuestionRuleDto>>();

        return new SuccessDataResult<List<CandidateQuestionRuleDto>>(toDto, Messages.ListedSuccess);
    }

    public async Task<IDataResult<CandidateQuestionRuleDto>> GetByExamRuleIdAsync(Guid examRuleId)
    {
        var questionRule = await _candidateQuestionRuleRepository.GetAllAsync(false);

        var filteredQuestionRules = questionRule.Where(rule => rule.CandidateExamRuleId == examRuleId).ToList();

        if (filteredQuestionRules is null)
        {
            return new ErrorDataResult<CandidateQuestionRuleDto>(Messages.QuestionRuleNotFound);
        }

        var toDto = filteredQuestionRules.Adapt<CandidateQuestionRuleDto>();

        return new SuccessDataResult<CandidateQuestionRuleDto>(toDto, Messages.FoundSuccess);
    }

    public async Task<IDataResult<CandidateQuestionRuleDto>> GetByIdAsync(Guid id)
    {
        var questionRule = await _candidateQuestionRuleRepository.GetByIdAsync(id);
        if (questionRule is null)
        {
            return new ErrorDataResult<CandidateQuestionRuleDto>(Messages.QuestionRuleNotFound);
        }
        var toDto = questionRule.Adapt<CandidateQuestionRuleDto>();

        return new SuccessDataResult<CandidateQuestionRuleDto>(toDto, Messages.FoundSuccess);
    }

    public Task<IDataResult<CandidateQuestionRuleDto>> UpdateAsync(CandidateQuestionRuleUpdateDto candidateQuestionRuleUpdateDto)
    {
        throw new NotImplementedException();
    }
}
