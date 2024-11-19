using BAExamApp.Dtos.Candidate.CandidateExamRule;
using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateQuestionRuleService
{
    Task<IDataResult<List<CandidateQuestionRuleDto>>> GetAllAsync();
    Task<IDataResult<CandidateQuestionRuleDto>> AddAsync(CandidateQuestionRuleCreateDto candidateQuestionRuleCreateDto);
    Task<IDataResult<CandidateQuestionRuleDto>> GetByIdAsync(Guid id);
    Task<IDataResult<CandidateQuestionRuleDto>> GetByExamRuleIdAsync(Guid examRuleId);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<CandidateQuestionRuleDto>> UpdateAsync(CandidateQuestionRuleUpdateDto candidateQuestionRuleUpdateDto);
}
