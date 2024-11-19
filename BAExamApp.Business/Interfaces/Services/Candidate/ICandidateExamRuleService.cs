using BAExamApp.Dtos.Candidate.CandidateExamRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateExamRuleService
{
    Task<IDataResult<List<CandidateExamRuleDto>>> GetAllAsync();
    Task<IDataResult<CandidateExamRuleDto>> AddAsync(CandidateExamRuleCreateDto candidateExamRuleCreateDto);
    Task<IDataResult<CandidateExamRuleDto>> GetByIdAsync(Guid id);
    Task<IDataResult<CandidateExamRuleDto>> GetByExamIdAsync(Guid examId);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<CandidateExamRuleDto>> UpdateAsync(CandidateExamRuleUpdateDto candidateExamRuleUpdateDto);

}
