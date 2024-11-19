using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateExamRule;
using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
using BAExamApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateExamRuleService : ICandidateExamRuleService
{
    private readonly IMapper _mapper;
    private readonly ICandidateExamRuleRepository _candidateExamRuleRepository;

    public CandidateExamRuleService(ICandidateExamRuleRepository candidateExamRuleRepository)
    {
        _candidateExamRuleRepository = candidateExamRuleRepository;
    }





    public async Task<IDataResult<CandidateExamRuleDto>> AddAsync(CandidateExamRuleCreateDto candidateExamRuleCreateDto)
    {
        var examRule = candidateExamRuleCreateDto.Adapt<CandidateExamRule>();

        await _candidateExamRuleRepository.AddAsync(examRule);
        await _candidateExamRuleRepository.SaveChangesAsync();

        var examRuleToDto = examRule.Adapt<CandidateExamRuleDto>();
        return new SuccessDataResult<CandidateExamRuleDto>(examRuleToDto, Messages.AddSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var examRule = await _candidateExamRuleRepository.GetByIdAsync(id);

        if (examRule is null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }

        examRule.CandidateQuestionRules.ToList().ForEach(q => q.Status = Core.Enums.Status.Deleted);

        examRule.Status = Core.Enums.Status.Deleted;

        await _candidateExamRuleRepository.DeleteAsync(examRule);
        await _candidateExamRuleRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<List<CandidateExamRuleDto>>> GetAllAsync()
    {
        var examRule = await _candidateExamRuleRepository.GetAllAsync(false);

        var toDto = examRule.Adapt<List<CandidateExamRuleDto>>();

        return new SuccessDataResult<List<CandidateExamRuleDto>>(toDto, Messages.ListedSuccess);
    }

    public async Task<IDataResult<CandidateExamRuleDto>> GetByExamIdAsync(Guid examId)
    {
        var examRule = await _candidateExamRuleRepository.GetAllAsync(false);

        var filteredExamRules = examRule.Where(rule => rule.CandidateExamId == examId).ToList();

        if (filteredExamRules is null)
        {
            return new ErrorDataResult<CandidateExamRuleDto>(Messages.ExamRuleNotFound);
        }

        var toDto = filteredExamRules.Adapt<CandidateExamRuleDto>();

        return new SuccessDataResult<CandidateExamRuleDto>(toDto, Messages.FoundSuccess);
    }

    public async Task<IDataResult<CandidateExamRuleDto>> GetByIdAsync(Guid id)
    {
        var examRule = await _candidateExamRuleRepository.GetByIdAsync(id);
        if (examRule is null)
        {
            return new ErrorDataResult<CandidateExamRuleDto>(Messages.ExamRuleNotFound);
        }
        var toDto = examRule.Adapt<CandidateExamRuleDto>();

        return new SuccessDataResult<CandidateExamRuleDto>(toDto, Messages.FoundSuccess);
    }

    public Task<IDataResult<CandidateExamRuleDto>> UpdateAsync(CandidateExamRuleUpdateDto entity)
    {
        throw new NotImplementedException();
    }
}
